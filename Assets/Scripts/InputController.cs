using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;

public class InputAction
{
    public string button;
    public Action action;
    public bool doubleClick;

    public InputAction(string button, Action action, bool doubleClick = false)
    {
        this.button = button;
        this.action = action;
        this.doubleClick = doubleClick;
    }
}

[RequireComponent(typeof(PlayerController))]
public class InputController : MonoBehaviour
{
    PlayerController playerController;
	MusicTempo mTempo;
    InputAction[] inputActions;

    InputAction engagedAction;

    float doubleClickDelay = 0.3f;

	void Start()
    {
        playerController = GetComponent<PlayerController>();

        inputActions = new InputAction[]
        {
            new InputAction("Jump", playerController.Jump),
            new InputAction("Stop", playerController.Stop),
            new InputAction("Walk", playerController.Walk),
            new InputAction("Walk", playerController.Sprint, true)
        };

        mTempo = SoundManager.Instance.musicTempo;
	}

    void ProcessActions()
    {
        if(engagedAction != null)
        {
            ProcessAction(engagedAction);
        }

        for(int i = 0; i < inputActions.Length; i++)
        {
            ProcessActionIfInTolerance(inputActions[i]);
        }
    }

    void ProcessAction(InputAction inputAction)
    {
        if(Input.GetButtonDown(inputAction.button))
            inputAction.action();
    }

    void ProcessActionIfInTolerance(InputAction inputAction)
    {
        if(Input.GetButtonDown(inputAction.button))
        {
            if(!mTempo.isInToleranceNow())
            {
                playerController.Hit();
            }
            else
            {
                if(inputAction.doubleClick)
                {
                    engagedAction = inputAction;
                    StartCoroutine(DisengageAction());
                }
                else
                    inputAction.action();
            }
        }
    }

    IEnumerator DisengageAction()
    {
        yield return new WaitForSeconds(doubleClickDelay);
        engagedAction = null;
    }

	// Update is called once per frame
	void Update ()
    {
        ProcessActions();
	}
}

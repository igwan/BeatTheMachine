﻿using UnityEngine;
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

    bool actionHappenedThisTempoKey;

	void Start()
    {
        playerController = GetComponent<PlayerController>();

        inputActions = new InputAction[]
        {
            new InputAction("Jump", playerController.Jump),
            new InputAction("Stop", playerController.Stop),
            new InputAction("Walk", playerController.Walk)
        };

        mTempo = SoundManager.Instance.musicTempo;
		mTempo.AddEndEvent(PostTempoKey);
	}

    void PostTempoKey()
    {
       // Debug.Log("PostTempoKey");
        if(!actionHappenedThisTempoKey)
            playerController.Hit();

        actionHappenedThisTempoKey = false;
    }

    void ProcessActions()
    {
        if(engagedAction != null)
        {
            ProcessAction(engagedAction);
        }

        for(int i = 0; i < inputActions.Length; i++)
        {
            actionHappenedThisTempoKey |= ProcessActionIfInTolerance(inputActions[i]);
        }
    }

    void ProcessAction(InputAction inputAction)
    {
        if(Input.GetButtonDown(inputAction.button))
            inputAction.action();
    }

    bool ProcessActionIfInTolerance(InputAction inputAction)
    {
        if(!Input.GetButtonDown(inputAction.button))
            return false;

        if(!mTempo.isInTolerance())
        {
            playerController.Hit();
        }

        if(inputAction.doubleClick)
        {
            engagedAction = inputAction;
            StartCoroutine(DisengageAction());
        }
        else
            inputAction.action();

        return true;
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

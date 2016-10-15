using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerController))]
public class InputController : MonoBehaviour
{
    PlayerController playerController;
	MusicTempo mTempo;

    Dictionary<string, Action> actions;

	void Start()
    {
        playerController = GetComponent<PlayerController>();
        actions = new Dictionary<string, Action>()
        {
            { "Jump", playerController.Jump },
            { "Stop", playerController.Stop },
            { "Walk", playerController.Walk },
            { "Sprint", playerController.Sprint }
        };

        mTempo = SoundManager.Instance.musicTempo;
	}

    void ProcessActions()
    {
        foreach(string inputName in actions.Keys)
        {
            if(Input.GetButtonDown(inputName))
            {
                if(!mTempo.isInToleranceNow())
                    Debug.Log("missed");
                else
                    actions[inputName]();
            }
        }
    }

	// Update is called once per frame

	void Update ()
    {
		if (Input.GetKeyDown ("space") && mTempo.isInToleranceNow ()) {
			Debug.Log ("gg");
		} else if(Input.GetKeyDown ("space")){
			Debug.Log ("noob");
		}

        ProcessActions();
	}
}

using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class GameManager : MonoBehaviour
{
    SKStateMachine<GameManager> stateMachine;

	// Use this for initialization
	void Start ()
    {
        CreateStateMachine();
	}

	// Update is called once per frame
	void Update ()
    {
        stateMachine.update(Time.deltaTime);
	}

    void CreateStateMachine()
    {
        stateMachine = new SKStateMachine<GameManager>(this, new GameState.IntroScreen());
        stateMachine.addState(new GameState.PreGame());
        stateMachine.addState(new GameState.Game());
        stateMachine.addState(new GameState.PostGame());
    }
}

using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using Prime31.StateKit;

public class GameManager : Singleton<MonoBehaviour>
{
    public UnityEvent EndGameEvent = new UnityEvent();

    int gameSpeed;

    SKStateMachine<GameManager> stateMachine;

    MusicTempo musicTempo;

	// Use this for initialization
	void Start ()
    {
        musicTempo = new MusicTempo();
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

    public void SetSpeed(int speed)
    {
        gameSpeed = speed;
        soundManager.Instance.SetSpeed(speed);
        musicTempo.ChangeSpeed(speed);
    }
}

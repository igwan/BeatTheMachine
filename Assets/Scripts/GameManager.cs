using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using Prime31.StateKit;

public class GameManager : Singleton<GameManager>
{
    public GameObject GameOverUI;
    public GameObject WinUI;
	public GameObject TitleScreen;

    public float postGameDuration = 3;
    public PlayerController player;

    public UnityEvent EndGameEvent = new UnityEvent();

    int gameSpeed;

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
        stateMachine.addState(new GameState.Win());
        stateMachine.addState(new GameState.GameOver());
		stateMachine.addState(new GameState.Restart());
    }

    public void SetSpeed(int speed)
    {
        gameSpeed = speed;
    }
}

using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;


//This class must be instantiate when the tempo begin
public class MusicTempo {


	/*
	  VOCABULARY :

            |---------------[-----|-----]----------------|
            ^                     ^----------------------^				
 	    tempo key                         tempo slot						  
                            ^-----------^
                             tolerance
	 */

	//Constructor
	public MusicTempo(){
		this.currentTempo = this.tempo; 
	}

    // FIXME: mettre les vrai valeurs
    [SerializeField] static int[] tempos = { 70, 90, 110, 130, 150, 170 };

    public static int GetPeriod(int speed)
    {
        return (tempos[speed] / 60) * 1000;
    }

	//unit : millliseconds
	//tempo time between two key point
	public int tempo = 1000;

	//tolerance between the key input and the tempo
	public int tolerance = 100 ;

	//current Tempo 
	private int currentTempo;


	//Tempo Key Event
	private UnityEvent tempoKeyEvent = new UnityEvent ();

	//Pre or Post Tempo Key Event
	private List<UnityEvent> preTempoKeyEvent = new List<UnityEvent>();
	//List milisecondes
	private List<int> preTempoKeyEventDelay = new List<int>();
	//List milisecondes
	private List<bool> preTempoKeyEventActivated = new List<bool>();

	private float beginTime;

	//Change the Tempo of the music
	public void ChangeTempo(int tempo){
		this.tempo = tempo ;
	}

    public void ChangeSpeed(int speed)
    {
        ChangeTempo(MusicTempo.GetPeriod(speed));
    }

	//put this function in a FixedUpdate to decremente time on the tempo
	public void UpdateCurrentTempo(){
		currentTempo -= (int)(Time.deltaTime*1000) ;
	}

	//return a bool if it is the tempo Key
	public bool isTempoKey(){
		return this.currentTempo < 0;
	}
		
	public void nextTempoSlot(){
		for (int i = 0; i < this.preTempoKeyEvent.Count; i++) {
			this.preTempoKeyEventActivated [i] = false;
		}
		this.currentTempo = tempo ;
	}

	// function to know if at a specific Time we are in the tolerance slot
	// how to use it : isInTolerance((int)((Time.time-beginTime)*1000))
	public bool isInTolerance(int clickTime){
		int clickInTempoRef = clickTime % tempo;
		return clickInTempoRef > (tempo - tolerance) || clickInTempoRef < tolerance  ;
	}


	// function to know if (for the currentTime when the function is called) it is in the tolerance slot
	public bool isInToleranceNow(){
		return isInTolerance ((int)((Time.time - beginTime) * 1000));
	}

	//For Observers Pattern
	public void addTempoKeyEvent(UnityAction action){
		tempoKeyEvent.AddListener (action);
	}

	public void addPreTempoKeyEvent(UnityAction action, int delay){
		UnityEvent preEvent = new UnityEvent ();
		preEvent.AddListener (action);
		this.preTempoKeyEvent.Add (preEvent);
		this.preTempoKeyEventDelay.Add (delay);
		this.preTempoKeyEventActivated.Add (false);
	}

	public void testEvents(){
		for (int i = 0; i < this.preTempoKeyEvent.Count; i++) {
			if (!this.preTempoKeyEventActivated[i] && this.currentTempo < this.preTempoKeyEventDelay [i]) {
				this.preTempoKeyEventActivated [i] = true;
				this.preTempoKeyEvent [i].Invoke ();
			}
		}
	}

	public void startTempo(){
		this.beginTime = Time.time ;
	}
		
		
	//Method to Call in a FixedUpdate Monobehaviour to snap the TempoKeyEvent if we are on a TempoKey
	public void  tempoProcess() {
		UpdateCurrentTempo ();
		if (isTempoKey ()) {
			nextTempoSlot ();
			tempoKeyEvent.Invoke ();
		}
	}
}

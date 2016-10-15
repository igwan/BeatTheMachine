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
	public int tempo = 2000;

	//tolerance between the key input and the tempo
	public int tolerance = 250 ;

	//current Tempo 
	private int currentTempo;


	//Tempo Key Event
	private UnityEvent tempoKeyEvent = new UnityEvent ();

	//Pre Tempo Key Event
	private List<Event> preTempoKeyEvent = new List<Event>();
	private List<Event> postTempoKeyEvent = new List<Event>();

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
		//currentTempo -= (int)(Time.deltaTime*1000) ;
		currentTempo = tempo - (int)(Time.time*1000) % tempo ;
		//Debug.Log (currentTempo);
	}

	//return a bool if it is the tempo Key
	public bool isTempoKey(){
		return this.currentTempo < 50;
	}
		
	public void nextTempoSlot(){
		this.currentTempo = tempo ;
	}

    public void nextTempoTolerance()
    {
		for (int i = 0; i < this.preTempoKeyEvent.Count; i++) {
			this.preTempoKeyEvent[i].activated = false;
		}

		for (int i = 0; i < this.postTempoKeyEvent.Count; i++) {
			this.postTempoKeyEvent[i].activated = false;
		}
    }

    public bool isTolerancePassed()
    {
        return tempo - currentTempo  < tolerance;
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
		this.preTempoKeyEvent.Add (new Event (action,delay));
	}

	public void addPostTempoKeyEvent(UnityAction action, int delay){
		this.postTempoKeyEvent.Add (new Event (action,delay));
	}

    public void addPostToleranceEvent(UnityAction action)
    {
        addPostTempoKeyEvent(action, tolerance);
    }

	public void testPreEvents(){
		for (int i = 0; i < this.preTempoKeyEvent.Count; i++) {
			if (!this.preTempoKeyEvent[i].activated && this.currentTempo < this.preTempoKeyEvent [i].delay ) {
				this.preTempoKeyEvent [i].activated = true;
				this.preTempoKeyEvent [i].myEvent.Invoke ();
			}
		}
	}

	public void testPostEvents(){
		for (int i = 0; i < this.postTempoKeyEvent.Count; i++) {
			if (!this.postTempoKeyEvent[i].activated && (this.tempo-this.currentTempo) < this.postTempoKeyEvent [i].delay ) {
                Debug.Log("testPostEvents");
				this.postTempoKeyEvent[i].activated = true;
				this.postTempoKeyEvent [i].myEvent.Invoke ();

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
		testPreEvents ();
		testPostEvents ();

        if(isTolerancePassed())
            nextTempoTolerance();
	}
}

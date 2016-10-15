using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;


//This class must be instantiate when the tempo begin
public class MusicTempo : MonoBehaviour
{

	/*
	  VOCABULARY :

            |---------------[-----|-----]----------------|
            ^                     ^----------------------^				
 	    tempo key                         tempo slot						  
                            ^-----------^
                             tolerance
	 */

    void Start()
    {
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


	//TEMPO

    public void ChangeSpeed(int speed)
    {
        ChangeTempo(MusicTempo.GetPeriod(speed));
    }


	//Change the Tempo of the music
	public void ChangeTempo(int tempo){
		this.tempo = tempo ;
	}

	//put this function in a FixedUpdate to decremente time on the tempo
	public void UpdateCurrentTempo(){
		//currentTempo = tempo - (int)(Time.time*1000) % tempo ;
        currentTempo -= (int)(Time.deltaTime * 1000);
	}

	//return a bool if it is the tempo Key
	public bool isTempoKey(){
		return this.currentTempo < 0;
	}

	public void nextTempoSlot(){
		this.currentTempo = tempo ;
        beforeKeyPoint = false;
	}


	public void startTempo(){
		this.beginTime = Time.time;
	}

    private bool beforeKeyPoint = true;


	//TOLERANCE
	private bool toleranceDone;

	private bool isInTolerance2 = false ;

	private UnityEvent toleranceBeginEvent = new UnityEvent ();

	private UnityEvent toleranceEndEvent = new UnityEvent ();

	public void AddBeginEvent(UnityAction action){
		this.toleranceBeginEvent.AddListener (action);
	}

	public void AddEndEvent(UnityAction action){
		this.toleranceEndEvent.AddListener (action);
	}

	public void toleranceBegin(){
		if(!isInTolerance2 && (currentTempo < tolerance)){
			Debug.Log ("debut");
			isInTolerance2 = true ;
			this.toleranceBeginEvent.Invoke ();
		}
	}

	public void toleranceEnd(){
		if (isInTolerance2 && (tempo - currentTempo) > tolerance && !beforeKeyPoint) {
			Debug.Log ("fin");
            beforeKeyPoint = true;
			isInTolerance2 = false;
			this.toleranceEndEvent.Invoke ();
		}
	}

	public bool isInTolerance(){
		return isInTolerance2;
	}

	//Events

	/*
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
			if (!this.postTempoKeyEvent[i].activated && (this.tempo-this.currentTempo) < this.postTempoKeyEvent [i].delay ) {            Debug.Log(string.Format("{0} {1} {2}", tempo, currentTempo, postTempoKeyEvent[i].delay));
				this.postTempoKeyEvent[i].activated = true;
				this.postTempoKeyEvent [i].myEvent.Invoke ();
			}
		}
	}
	*/

		
	//Method to Call in a FixedUpdate Monobehaviour to snap the TempoKeyEvent if we are on a TempoKey
	public void  tempoProcess() {
		UpdateCurrentTempo ();
		if (isTempoKey ())
        {
			nextTempoSlot ();
			tempoKeyEvent.Invoke ();
            toleranceDone = false;
		}
		toleranceBegin();
		toleranceEnd ();
	}

    void FixedUpdate()
    {
        tempoProcess();
    }
}

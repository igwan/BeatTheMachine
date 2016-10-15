using System;
using UnityEngine ;
using UnityEngine.Events;

public class Event
{
	public UnityEvent myEvent ;
	public int delay ;
	public bool activated ;

	Event(UnityAction action, int delay){
		this.activated = false;
		this.delay = delay;
		this.myEvent = new UnityEvent();
		this.myEvent.AddListener (action);
	}


}



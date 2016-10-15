﻿using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public class DeathEvent : UnityEvent {};
    public DeathEvent Die = new DeathEvent();

    bool hasArmor = true;

	//speed
	public float speed = 1f ;

	//distance between Tile 
	private float distance ;

	private Vector3 targetPosition ;


	void Start(){
		this.distance = MapManager.Instance.tileLength;
		targetPosition = this.transform.position;
	}

	private bool mustMove(){
		return this.transform.position != this.targetPosition;
	}

	public void MoveToTarget(){
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, targetPosition, step);
	}

	public void Walk(){
		//set Objectif 
		targetPosition = transform.position + new Vector3(distance,0,0);
		//Launch Animation
		Debug.Log("Walk");
	}

    public void Jump()
    {
        Debug.Log("Jump");
    }

    public void Sprint()
    {
        Debug.Log("Sprint");
    }

    public void Stop()
    {
        Debug.Log("Stop");

    }


    public void Hit()
    {
        if(hasArmor)
        {
            hasArmor = false;
        }
        else
        {
            Die.Invoke();
        }

    }

	void FixedUpdate(){
		if (this.mustMove ()) {
			this.MoveToTarget ();
		}
		if (Input.anyKeyDown)
			Walk ();
	}
}

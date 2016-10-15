using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class InputController : MonoBehaviour {

	MusicTempo mTempo ;

	void Start(){
		mTempo = new MusicTempo ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("right")) {
			Debug.Log ("toto");
		}
	}
}

using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
public class InputController : MonoBehaviour
{
	MusicTempo mTempo ;

	void Start(){
		mTempo = new MusicTempo ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space") && mTempo.isInToleranceNow ()) {
			Debug.Log ("gg");
		} else if(Input.GetKeyDown ("space")){
			Debug.Log ("noob");
		}
	}
}

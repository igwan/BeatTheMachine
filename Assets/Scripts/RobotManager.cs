using UnityEngine;
using System.Collections;

public class RobotManager : MonoBehaviour {

	private MusicTempo mTempo ;
	public int delay ;

	void Start () {
		mTempo = SoundManager.Instance.musicTempo;
		mTempo.addPreTempoKeyEvent ();
	}
	
	void Update () {
	
	}
}

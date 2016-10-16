using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RobotManager : MonoBehaviour {

	private MusicTempo mTempo ;
	public int delay ;
	private Dictionary<GameObject,Animator> robots = new Dictionary<GameObject,Animator> ();


	void Start () {
		mTempo = SoundManager.Instance.musicTempo;
		//mTempo.addPreTempoKeyEvent (beginAnimation,delay);
		mTempo.AddBeginEvent(beginAnimation);
		//FIXME : do properly a Find of all the robots
		GameObject[] robotstab=  GameObject.FindGameObjectsWithTag("FlexRobot");
		for (int i = 0; i < robotstab.Length; i++) {
			robots.Add (robotstab [i], robotstab [i].GetComponent<Animator> ());
		}

		GameObject.Find ("Player").GetComponent<PlayerController> ().AddDeathEvent (robotsKill);
	}

	void addRobot(GameObject robot){
		robots.Add (robot, robot.GetComponent<Animator> ());
	}

	void deleteRobot(GameObject robot){
		robots.Remove (robot);
		Destroy (robot);
	}

	void beginAnimation(){
		//launch animation flex for each robot
		foreach (KeyValuePair<GameObject,Animator> pair in robots) {
			pair.Value.SetTrigger ("Flex");
		}
	}

	void robotsKill(){
		foreach (KeyValuePair<GameObject,Animator> pair in robots) {
			pair.Value.SetTrigger ("Die");
		}
	}
}

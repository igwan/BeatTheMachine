using UnityEngine;
using System.Collections;

public class RobotController : MonoBehaviour {
	public MapManager mapManager;
	public RobotManager robotManager;

	void Start(){
		robotManager = gameObject.transform.parent.GetComponent<RobotManager> ();
		mapManager = GameObject.Find ("MapManager").GetComponent<MapManager> ();
	}

	void OnTriggerExit2D(Collider2D collidingObject)
	{
		Debug.Log ("On trigger exit");

		if (collidingObject.gameObject.CompareTag ("CameraCrowdColliderRow1")) 
		{

			mapManager.AddNextCrowdRobot (transform.position, transform.parent);

			robotManager.deleteRobot (gameObject);
			//Destroy (gameObject);
		}
	}

/*	void OnTriggerStay2D(Collider2D coll)
	{
		Debug.Log ("Stay");
	}*/
}

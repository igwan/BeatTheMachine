using UnityEngine;
using System.Collections;

public class LevelSegmentController : MonoBehaviour {
	public MapManager mapManager;

	void OnTriggerExit2D(Collider2D collidingObject)
	{
		Debug.Log (MapManager.Instance.levelSegments.Length);
		if (collidingObject.gameObject.CompareTag ("CameraFrontCollider")) 
		{
			//Debug.Log ("toto");

			mapManager.AddNextLevelSegment (transform.localPosition);
			Destroy (gameObject);
		}
		Debug.Log ("trigger exit");
	}

	void OnTriggerEnter2D(Collider2D collidingObject)
	{
		//Debug.Log ("trigger enter");
	}

	void OnTriggerStay2D(Collider2D collidingObject)
	{
		//Debug.Log ("trigger stay");
	}
}
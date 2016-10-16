using UnityEngine;
using System.Collections;

public class BackgroundSegmentController : MonoBehaviour {
	public MapManager mapManager;

	void OnTriggerExit2D(Collider2D collidingObject)
	{
		if (collidingObject.CompareTag ("CameraBackgroundCollider")) 
		{
			//Debug.Log ("OnTriggerExit");
			mapManager.AddNextBackgroundSegment (transform.localPosition);
			Destroy (gameObject);
		}
	}
}

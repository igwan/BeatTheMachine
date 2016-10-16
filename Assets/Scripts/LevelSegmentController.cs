using UnityEngine;
using System.Collections;

public class LevelSegmentController : MonoBehaviour {
	public MapManager mapManager;

	void OnTriggerExit2D(Collider2D collidingObject)
	{
		if (collidingObject.gameObject.CompareTag ("CameraFrontCollider")) 
		{
			mapManager.AddNextLevelSegment (transform.localPosition);
			Destroy (gameObject);
		}
	}
}
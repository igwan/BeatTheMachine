using UnityEngine;
using System.Collections;

public class BackgroundSegmentController : MonoBehaviour {
	public MapManager mapManager;

	void OnTriggerExit2D(Collider2D collidingObject)
	{
		if (collidingObject.CompareTag ("CameraBakgroundCollider")) 
		{
			mapManager.AddNextBackgroundSegment (transform.localPosition);
			Destroy (gameObject);
		}
	}
}

using UnityEngine;
using System.Collections;

public class LevelSegmentController : MonoBehaviour {
	public MapManager mapManager;

	void OnTriggerExit2D(Collider2D collidingObject)
	{
		if (collidingObject.CompareTag ("MainCamera")) 
		{
			mapManager.AddNextLevelSegment (transform.localPosition);
			Destroy (this);
		}
	}
}
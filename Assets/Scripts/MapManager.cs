using UnityEngine;
using System.Collections;

public class MapManager : Singleton<MapManager> {

	public LevelSegmentController[] levelSegments;
	Camera camera;
	public float levelSegmentsWidth = 10;

	public float tileLength = 1 ;

	void Start()
	{
		Debug.Log (levelSegments.Length);
	}

	public void AddNextLevelSegment(Vector3 exitingSegmentPosition)
	{
		int randomIndex = Random.Range (0, MapManager.Instance.levelSegments.Length);
		Debug.Log ("ExitingSegmentPos :" + exitingSegmentPosition);

		Vector3 aimedPosition = new Vector3 (exitingSegmentPosition.x + (3*MapManager.Instance.levelSegmentsWidth), exitingSegmentPosition.y, exitingSegmentPosition.z);
		Debug.Log ("aimedPos: "+aimedPosition);

		Instantiate (MapManager.Instance.levelSegments [randomIndex], aimedPosition, Quaternion.identity);
	}

	void Update(){
		Debug.Log ("update " + levelSegments.Length);
	}
}

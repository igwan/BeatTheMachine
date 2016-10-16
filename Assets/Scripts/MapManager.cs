using UnityEngine;
using System.Collections;

public class MapManager : Singleton<MapManager> {

	Camera camera;

	public LevelSegmentController[] levelSegments;
	public float levelSegmentsWidth = 10;

	public BackgroundSegmentController[] backgroundSegments;
	public float backgroundSegmentsWidth = 15;

	public float tileLength = 1 ;

	void Start()
	{
		Debug.Log (levelSegments.Length);
	}

	public void AddNextLevelSegment(Vector3 exitingSegmentPosition)
	{
		int randomIndex = Random.Range (0, MapManager.Instance.levelSegments.Length);

		Vector3 aimedPosition = new Vector3 (exitingSegmentPosition.x + (3*MapManager.Instance.levelSegmentsWidth), exitingSegmentPosition.y, exitingSegmentPosition.z);

		Instantiate (MapManager.Instance.levelSegments [randomIndex], aimedPosition, Quaternion.identity);
	}

	public void AddNextBackgroundSegment(Vector3 exitingSegmentPosition)
	{
		int randomIndex = Random.Range (0, MapManager.Instance.backgroundSegments.Length);

		Vector3 aimedPosition = new Vector3 (exitingSegmentPosition.x + (3*MapManager.Instance.levelSegmentsWidth), exitingSegmentPosition.y, exitingSegmentPosition.z);

		Instantiate (MapManager.Instance.backgroundSegments [randomIndex], aimedPosition, Quaternion.identity);
	}
}
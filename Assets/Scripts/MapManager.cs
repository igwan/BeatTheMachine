using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class MapManager : Singleton<MapManager>
{
    // maybe set a max segment count and call EndLevel when reached
    public class EndLevelEvent : UnityEvent {};
    public EndLevelEvent EndLevel = new EndLevelEvent();

	Camera camera;

	public LevelSegmentController[] levelSegments;
	public float levelSegmentsWidth = 10;
	public GameObject foreground;

	public BackgroundSegmentController[] backgroundSegments;
	public float backgroundSegmentsWidth = 15;
	public GameObject background;


	public float tileLength = 1 ;

	public void AddNextLevelSegment(Vector3 exitingSegmentPosition)
	{
		int randomIndex = Random.Range (0, MapManager.Instance.levelSegments.Length);

		Vector3 aimedPosition = new Vector3 (exitingSegmentPosition.x + (3*MapManager.Instance.levelSegmentsWidth), exitingSegmentPosition.y, exitingSegmentPosition.z);

		LevelSegmentController newSegment = (LevelSegmentController) Instantiate (MapManager.Instance.levelSegments [randomIndex], aimedPosition, Quaternion.identity);

		//newSegment.transform.SetParent(foreground.transform);
	}

	public void AddNextBackgroundSegment(Vector3 exitingSegmentPosition)
	{
		int randomIndex = Random.Range (0, MapManager.Instance.backgroundSegments.Length);

		Vector3 aimedPosition = new Vector3 (exitingSegmentPosition.x + (6*MapManager.Instance.backgroundSegmentsWidth), exitingSegmentPosition.y, exitingSegmentPosition.z);

		BackgroundSegmentController newBackgroundSegment = (BackgroundSegmentController) Instantiate (MapManager.Instance.backgroundSegments [randomIndex], aimedPosition, Quaternion.identity);

		//newBackgroundSegment.transform.parent = background.transform;
	}
}

﻿using UnityEngine;
using System.Collections;

public class MapManager : Singleton<MapManager> {

	public GameObject[] levelSegments;
	Camera camera;
	public float levelSegmentsWidth  = 15;

	public float tileLength = 1 ;

	public void AddNextLevelSegment(Vector3 exitingSegmentPosition)
	{
		int randomIndex = Random.Range (0, levelSegments.Length);
		Vector3 aimedPosition = new Vector3 (exitingSegmentPosition.x + 3 * levelSegmentsWidth, exitingSegmentPosition.y, exitingSegmentPosition.z);
		Instantiate (levelSegments [randomIndex], aimedPosition, Quaternion.identity);
	}
}

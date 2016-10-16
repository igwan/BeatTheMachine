using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{

	private float cameraOffsetX;
	public GameObject player;

	void Start () 
	{		
		cameraOffsetX = transform.position.x - player.transform.position.x;
	}

	void LateUpdate () 
	{
		transform.position = new Vector3(player.transform.position.x + cameraOffsetX, transform.position.y, transform.position.z);
	}
}
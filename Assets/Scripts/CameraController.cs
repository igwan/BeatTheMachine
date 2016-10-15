using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{

	private float cameraOffset;
	public GameObject player;

	void Start () 
	{		
		cameraOffset = transform.position.x - player.transform.position.x;
	}

	void LateUpdate () 
	{
		transform.position = new Vector3(player.transform.position.x + cameraOffset, transform.position.y, transform.position.z);
	}
}
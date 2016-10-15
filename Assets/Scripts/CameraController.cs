using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{

	private float offset;
	public GameObject player;

	// Use this for initialization
	void Start () 
	{		
		offset = transform.position.x - player.transform.position.x;
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		transform.position = new Vector3(player.transform.position.x + offset, player.transform.position.y, player.transform.position.z);
	}
}
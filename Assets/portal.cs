using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal : MonoBehaviour {

	[SerializeField]
	public string portalDestination;

	[SerializeField]
	public int offset;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			Debug.Log ("entering portal");
			var startPortal = GameObject.Find (portalDestination);
			var player = other.gameObject;
			var camera = GameObject.Find ("Main Camera");

			var destinationVector = startPortal.transform.position;
			destinationVector.x += offset;
			var playerVector = player.transform.position;
			Debug.Log ("startVector.x: " + destinationVector.x);
			player.transform.position = new Vector3(destinationVector.x, playerVector.y+0.2f, playerVector.z);
//			playerVector.Set (startVector.x, playerVector.y, playerVector.z);
			var cameraVector = camera.transform.position;
			camera.transform.position = new Vector3(destinationVector.x, cameraVector.y, cameraVector.z);
//			cameraVector.Set (startVector.x, cameraVector.y, cameraVector.z);
		}
	}
}

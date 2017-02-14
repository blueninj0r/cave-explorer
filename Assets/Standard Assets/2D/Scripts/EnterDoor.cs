using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class EnterDoor : MonoBehaviour {

	CameraFade cameraFade;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("HI");
		CameraFade.StartAlphaFade (Color.black, false, 3f);
	}

}

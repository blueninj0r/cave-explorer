using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

	[SerializeField]
	public int nextLevel;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SetLevel(int level){
		nextLevel = level;
		Debug.Log ("im exit number: " + level);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player" && enabled)
		{
			CameraFade.StartAlphaFade (Color.black, false, 3f);



//			var camera = GameObject.Find ("MainCamera");
//			camera.SendMessage ("ChangeLevel", nextLevel);
			//Application.LoadLevel(nextLevel);
		}
	}
}

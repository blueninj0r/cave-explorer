using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

[RequireComponent(typeof(Croaking))]
public class CharacterBrain : MonoBehaviour, INoteProcessor {

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		float h = CrossPlatformInputManager.GetAxis("Horizontal");
		if (h != 0.0f) {
			anim.SetBool ("Moving", true);
		} else {
			anim.SetBool ("Moving", false);
		}

	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "ground") {
			Debug.Log ("Set wet to false");
			anim.SetBool ("Wet", false);
		} else {
			Debug.Log ("Set wet to true");
			anim.SetBool ("Wet", true);
		}
	}

	public void ProcessNote(Notes note){
		var croak = this.gameObject.GetComponent<Croaking> ();
		croak.PlayNote (note);
		Debug.Log ("HELLO CHARACTER BRAIN");
	}
}

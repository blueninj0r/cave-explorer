using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Croaking))]
public class CharacterBrain : MonoBehaviour, INoteProcessor {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ProcessNote(Notes note){
		var croak = this.gameObject.GetComponent<Croaking> ();
		croak.PlayNote (note);
		Debug.Log ("HELLO CHARACTER BRAIN");
	}
}

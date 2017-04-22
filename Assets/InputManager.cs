using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class InputManager : MonoBehaviour {
	public GameObject[] frogs;
	public int timeSinceLastKey = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	void Update(){

		timeSinceLastKey++;

		if (timeSinceLastKey > 80) {
			var jPressed = Input.GetKeyUp (KeyCode.J);
			var kPressed = Input.GetKeyUp (KeyCode.K);
			var lPressed = Input.GetKeyUp (KeyCode.L);
			var mPressed = Input.GetKeyUp (KeyCode.M);

			if (jPressed) {
				SendNoteEvent(Notes.J);
				timeSinceLastKey = 0;
			} else if (kPressed) {
				SendNoteEvent(Notes.K);
				timeSinceLastKey = 0;
			} else if (lPressed) {
				SendNoteEvent(Notes.L);
				timeSinceLastKey = 0;
			} else if (mPressed) {
				SendNoteEvent(Notes.M);
				timeSinceLastKey = 0;
			}
		} else {
			timeSinceLastKey++;
		}
	}

	void SendNoteEvent(Notes note){
		frogs.ToList ().ForEach (f => ExecuteEvents.Execute<INoteProcessor> (f, null, (x, y) => x.ProcessNote (note)));
	}
}

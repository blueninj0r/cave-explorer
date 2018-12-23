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

		if (timeSinceLastKey > 15) {
			var cPressed = Input.GetKeyUp (KeyCode.C);
			var dPressed = Input.GetKeyUp (KeyCode.D);
			var ePressed = Input.GetKeyUp (KeyCode.E);
			var gPressed = Input.GetKeyUp (KeyCode.G);


			if (cPressed) {
				SendNoteEvent(Notes.C);
				timeSinceLastKey = 0;
			} else if (dPressed) {
				SendNoteEvent(Notes.D);
				timeSinceLastKey = 0;
			} else if (ePressed) {
				SendNoteEvent(Notes.E);
				timeSinceLastKey = 0;
			} else if (gPressed) {
				SendNoteEvent(Notes.G);
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

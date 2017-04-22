using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Linq;
using System.Text;
using UnityEngine.EventSystems;


public enum Notes {
	J,K,L,M
}

interface INoteProcessor : IEventSystemHandler{
	void ProcessNote (Notes note);
}	

public class EmmaFrogBrain : MonoBehaviour,INoteProcessor {

	private IList<Notes> noteMemory = new List<Notes>();
	private int cyclesSinceLastNote = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		cyclesSinceLastNote++;

		if (cyclesSinceLastNote > 200) {
			noteMemory = new List<Notes>();
		}
	}

	public void ProcessNote(Notes note){
		Debug.Log ("EmmaFrog is processing the note");
		noteMemory.Add (note);	
		cyclesSinceLastNote = 0;
		PrettyPrintList (noteMemory);
		var theSong = new Song ();
		if (theSong.IsEqual (noteMemory)) {
			Debug.Log ("I am impressed");
			//record something about impressed-ness
			noteMemory = new List<Notes>();
		}else {
			Debug.Log("I am not impressed yet.");
		}

		// is the song in the right order?
		// am I more impressed?
		// am I completely impressed yet?
	}

	bool isKeyPressed(bool key=false, bool key2=false, bool key3=false, bool key4=false) {
		return key || key2 || key3 || key4;
	}

	void PrettyPrintList<T>(IList<T> theList) {
			var sb = new StringBuilder ();
			sb.Append ("[");
			foreach (var item in theList) {
			sb.AppendFormat("{0}, ", item.ToString());
			}
			sb.Append("]");
			Debug.Log (sb.ToString ());

	}
}

public class Song{
	IList<Notes> songNotes;

	public Song(){
		songNotes = new List<Notes>{Notes.J,Notes.J,Notes.J,Notes.J,Notes.J,Notes.J,Notes.J,Notes.J};

	}

	public bool IsEqual(IList<Notes> notes){
		return notes.SequenceEqual (songNotes);
	}
}

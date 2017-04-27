using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;



public class EmmaFrogBrain : MonoBehaviour,INoteProcessor {

	private IList<Notes> noteMemory = new List<Notes>();
	private int cyclesSinceLastNote = 0;

	public IList<Song> Songs { get; set; }
	private int currentSong;

	// Use this for initialization
	void Start () {
		currentSong = 0;
	}
	
	// Update is called once per frame
	void Update () {
		cyclesSinceLastNote++;

		if (cyclesSinceLastNote > 200) {
			noteMemory = new List<Notes>();
		}
	}

	public void SetSongs(IList<Song> songs){
		Songs = songs;
	}

	public void ProcessNote(Notes note){
		Debug.Log ("EmmaFrog is processing the note");
		noteMemory.Add (note);	
		cyclesSinceLastNote = 0;
		PrettyPrintList (noteMemory);
		var theSong = Songs [currentSong];
		if (theSong.IsEqual (noteMemory)) {
			currentSong++;
			if (currentSong == Songs.Count) {
				Debug.Log ("I am so impressed");
				//endGame
			} else {
				Debug.Log ("carry on, sir");
			}

		} else {			
			Debug.Log("I am not impressed yet.");
		}
		if (noteMemory.Count == 8) {
			noteMemory = new List<Notes>();
		}
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



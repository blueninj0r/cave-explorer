using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Croaking))]
public class SingSong : MonoBehaviour, INoteProcessor {

	public Song MySong { get; set; }

	private bool playing = false;
	private IList<Notes> noteMemory = new List<Notes>();

	private int cyclesSinceLastNote = 0;

	private bool isListening;

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

	public void SetMySong(Song song){
		MySong = song;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player" && !playing) {
			StartCoroutine (SingTheSong ());
			isListening = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			Debug.Log ("cake");
			isListening = false;
			noteMemory = new List<Notes> ();
		}
	}

	private IEnumerator SingTheSong(){
		playing = true;
		foreach (var note in MySong.SongNotes) {
			var croak = this.gameObject.GetComponent<Croaking> ();
			croak.PlayNote (note);
			yield return new WaitForSeconds(MySong.NoteDuration);
		}
		playing = false;
	}

	public void ProcessNote(Notes note){
		if (isListening) {
			Debug.Log ("EmmaFrog is processing the note");
			noteMemory.Add (note);	
			cyclesSinceLastNote = 0;
			var theSong = MySong;
			if (theSong.IsEqual (noteMemory)) {
				Debug.Log ("I am so impressed");
				StartCoroutine (RespondToSong (Notes.G));
			} else {				
				Debug.Log ("carry on, sir");
				StartCoroutine (SingTheSong ());
			}
			noteMemory = new List<Notes> ();
			if (noteMemory.Count == 8) {
				//TODO: add unsatisfied feedback - plus New COLOURRRR!!!!!!
				noteMemory = new List<Notes> ();
			}
		}
	}

	private IEnumerator RespondToSong(Notes note){
		var croak = this.gameObject.GetComponent<Croaking> ();
		yield return new WaitForSeconds(0.5f);
		croak.PlayNote (note);
	}
}

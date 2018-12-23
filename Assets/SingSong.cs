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

	private bool isCompleted;

	// Use this for initialization
	void Start () {
		isCompleted = false;
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
			if (MySong.Count == noteMemory.Count) {
				if (theSong.IsEqual (noteMemory)) {
					Debug.Log ("sounds good");
					StartCoroutine (PlayChord ());
					var camera = GameObject.Find ("Main Camera");
					if (!isCompleted) {
						camera.SendMessage ("SongCompleted", MySong);
						isCompleted = true;
					}
				} else {				
					Debug.Log ("not good");
					StartCoroutine (SingTheSong ());
				}
				noteMemory = new List<Notes> ();
			}
		}
	}

	private IEnumerator PlayChord(){
		var croak = this.gameObject.GetComponent<Croaking> ();
		yield return new WaitForSeconds(0.5f);
		croak.PlayNote (Notes.C);
		croak.PlayNote (Notes.G);
		croak.PlayNote (Notes.E);
		yield return new WaitForSeconds(0.5f);
		croak.PlayNote (Notes.C);
		croak.PlayNote (Notes.G);
		croak.PlayNote (Notes.E);
	}
}

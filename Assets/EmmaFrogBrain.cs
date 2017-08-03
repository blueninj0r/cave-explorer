using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Croaking))]
public class EmmaFrogBrain : MonoBehaviour,INoteProcessor {

	private IList<Notes> noteMemory = new List<Notes>();
	private int cyclesSinceLastNote = 0;

	public IList<Song> Songs { get; set; }
	private int currentSong;

	private bool isListening;

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
		if (isListening) {
			Debug.Log ("EmmaFrog is processing the note");
			noteMemory.Add (note);	
			cyclesSinceLastNote = 0;
			PrettyPrintList (noteMemory);
			var theSong = Songs [currentSong];
			if (theSong.IsEqual (noteMemory)) {
				currentSong++;
				if (currentSong == Songs.Count) {
					Debug.Log ("I am so impressed");
					StartCoroutine (WinGame ());
				} else {
					Debug.Log ("carry on, sir");
					StartCoroutine(RespondToSong (Notes.D));				}
				noteMemory = new List<Notes> ();
			} else {			
				Debug.Log ("I am not impressed yet.");
			}
			if (noteMemory.Count == 8) {
				//TODO: add unsatisfied feedback - plus New COLOURRRR!!!!!!
				noteMemory = new List<Notes> ();
			}
		}
	}

	private IEnumerator WinGame(){
		var camera = GameObject.FindObjectOfType<Camera> ();
		camera.orthographicSize = 3;
		System.Threading.Thread.Sleep (1000);
		foreach (var song in Songs) {
			foreach (var note in song.SongNotes) {
				var croak = this.gameObject.GetComponent<Croaking> ();
				croak.PlayNote (note);
				yield return new WaitForSeconds (Songs [0].NoteDuration);
			}
		}
		//SceneManager.LoadScene("endgame");
	}

	private IEnumerator RespondToSong(Notes note){
		var croak = this.gameObject.GetComponent<Croaking> ();
		yield return new WaitForSeconds(0.5f);
		croak.PlayNote (note);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			isListening = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			isListening = false;
			currentSong = 0;
			noteMemory = new List<Notes> ();
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
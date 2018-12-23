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
	private int songsSung;

	private bool isListening;

	// Use this for initialization
	void Start () {
		songsSung = 0;
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
			if (Songs.Any(song => song.IsEqual (noteMemory))) {
				songsSung++;
				if (songsSung == Songs.Count) {
					Debug.Log ("I am so impressed");
					StartCoroutine(WinGame ());
				} else {
					Debug.Log ("carry on, sir");
					StartCoroutine(PlayChord ());				}
					noteMemory = new List<Notes> ();
			} else if (Songs[0].Count == noteMemory.Count) {			
				Debug.Log ("I am not impressed yet.");
				StartCoroutine(PlayNote ());
				noteMemory = new List<Notes> ();
			}
		}
	}

	private IEnumerator WinGame(){
		yield return new WaitForSeconds(2.0f);
		SceneManager.LoadScene ("endgame");
	}


	private IEnumerator PlayNote(){
		var croak = this.gameObject.GetComponent<Croaking> ();
		yield return new WaitForSeconds(0.5f);
		croak.PlayNote (Notes.D);
	}

	private IEnumerator PlayChord(){
		var croak = this.gameObject.GetComponent<Croaking> ();
		yield return new WaitForSeconds(0.5f);
		croak.PlayNote (Notes.E);
		croak.PlayNote (Notes.C);
		croak.PlayNote (Notes.G);
		yield return new WaitForSeconds(0.5f);
		croak.PlayNote (Notes.C);
		croak.PlayNote (Notes.G);
		croak.PlayNote (Notes.E);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			isListening = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			isListening = false;
			songsSung = 0;
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
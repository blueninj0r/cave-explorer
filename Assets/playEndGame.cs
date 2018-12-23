using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Croaking))]
public class playEndGame : MonoBehaviour {

	public IList<Song> Songs { get; set; }	
	private bool hasPlayed = false;

	// Use this for initialization
	void Start () {
		Songs = new List<Song> ();
		Songs.Add (new Song { SongNotes = new List<Notes> { Notes.C, Notes.C, Notes.D, Notes.G, Notes.C }, NoteDuration = 1 });
		Songs.Add (new Song { SongNotes = new List<Notes> { Notes.D, Notes.G, Notes.D, Notes.C, Notes.D }, NoteDuration = 1 });
		Songs.Add (new Song { SongNotes = new List<Notes> { Notes.D, Notes.G, Notes.C, Notes.E, Notes.E }, NoteDuration = 1 });

	}
	
	// Update is called once per frame
	void Update () {
		StartCoroutine(this.WinGame ());
	}

	private IEnumerator WinGame(){
		if (!hasPlayed) {
			hasPlayed = true;
			Debug.Log ("Hello, the game is ending - WInGame");
			foreach (var song in Songs) {
				foreach (var note in song.SongNotes) {
					var croak = this.gameObject.GetComponent<Croaking> ();
					croak.PlayNote (note);
					yield return new WaitForSeconds (Songs [0].NoteDuration);
				}
			}
			yield return new WaitForSeconds(2.0f);
			Application.Quit ();
		}
	}
}

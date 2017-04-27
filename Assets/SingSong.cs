using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Croaking))]
public class SingSong : MonoBehaviour {

	public Song MySong { get; set; }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetMySong(Song song){
		MySong = song;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			StartCoroutine (singTheSong ());			
		}
	}

	private IEnumerator singTheSong(){
		foreach (var note in MySong.SongNotes) {
			var croak = this.gameObject.GetComponent<Croaking> ();
			croak.ProcessNote (note);
			yield return new WaitForSeconds(MySong.NoteDuration);
		}
	}
}

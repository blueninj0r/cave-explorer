﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class GameManager : MonoBehaviour {

	public GameObject FullSongs;

	public GameObject[] OneSongEach;

	private IList<Song> songs = new List<Song>();

	// Use this for initialization
	void Start () {
		for (int i = 0; i < OneSongEach.Length; i++) {
			List<Notes> notes = null;
			if (i == 0) {
				notes = new List<Notes> { Notes.C, Notes.C, Notes.C, Notes.C, Notes.C, Notes.C, Notes.C, Notes.C };
			}
			if (i == 1) {
				notes = new List<Notes> { Notes.D, Notes.D, Notes.C, Notes.D, Notes.D, Notes.C, Notes.D, Notes.D };
			}

			var song = new Song {
				SongNotes = notes,
				NoteDuration = 1
			};
			OneSongEach [i].SendMessage ("SetMySong", song);
			songs.Add (song);
		}

		FullSongs.SendMessage ("SetSongs", songs);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

interface INoteProcessor : IEventSystemHandler{
	void ProcessNote (Notes note);
}

public enum Notes {
	C,D,G,E
}

public class Song{
	
	public IList<Notes> SongNotes { get; set; }

	public int NoteDuration { get; set; }

	public bool IsEqual(IList<Notes> notes){
		return notes.SequenceEqual (SongNotes);
	}
}
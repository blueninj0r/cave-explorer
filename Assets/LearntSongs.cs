using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LearntSongs : MonoBehaviour {

	private HashSet<Song> completedSongs;

	public Sprite[] noteSprites;
	public GameObject noteBubble;
	public Font textFont;

	private IList<GameObject> drawNotes;

	private float xOffset;
	private float yOffset;
	private Canvas canvas;

	// Use this for initialization
	void Start () {
		completedSongs = new HashSet<Song> ();
		drawNotes = new List<GameObject> ();

		yOffset = Screen.height - 50;
		xOffset = 60f;
		canvas = GameObject.FindObjectOfType<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
	}


	public void SongCompleted(Song song) {
		completedSongs.Add (song);

		// draw the songs that exist
		foreach (var note in song.SongNotes) {
			GameObject NewObj = new GameObject(); //Create the GameObject
			Image NewImage = NewObj.AddComponent<Image>(); //Add the Image Component script
			NewImage.sprite = noteSprites [(int)note]; //Set the Sprite of the Image Component on the new GameObject
			NewObj.GetComponent<RectTransform>().SetParent(canvas.transform); //Assign the newly created Image GameObject as a Child of the Parent Panel.
			NewObj.transform.position = new Vector3(xOffset,yOffset);
			NewObj.SetActive(true); //Activate the GameObject
			xOffset += 50f;
			drawNotes.Add (NewObj);
		}

		xOffset += 100f;
	}

}

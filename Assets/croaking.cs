using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Croaking : MonoBehaviour {

	public Sprite[] noteSprites;
	public AudioClip[] noteClips;
	public GameObject noteBubble;
	AudioSource audio;
	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
		
	}

	public void PlayNote(Notes note){
		var croak = noteClips[(int)note];
		audio.PlayOneShot (croak, 0.7F);
		var sprite = noteSprites[(int)note];
		var renderer = noteBubble.GetComponent<SpriteRenderer> ();
		renderer.sprite = sprite;
		Instantiate (noteBubble, this.gameObject.transform.position, Quaternion.identity);
	}

	void OnCroak()
	{
		
	}
}
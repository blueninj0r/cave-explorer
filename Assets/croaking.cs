using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Croaking : MonoBehaviour, INoteProcessor {

	public GameObject noteBubble;
	public AudioClip croak;
	AudioSource audio;
	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
		
	}
	
	// Update is called once per frame
	void Update () {
		// ToDo: determine if speechbubble should be destroyed		
	}

	public void ProcessNote(Notes note){
		audio.PlayOneShot (croak, 0.7F);
		Instantiate (noteBubble, this.gameObject.transform.position, Quaternion.identity);
	}

	void OnCroak()
	{
		
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleUp : MonoBehaviour {

	public float lifeTime;

	// Use this for initialization
	void Start () {
		Destroy(this.gameObject, lifeTime);
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.transform.Translate (new Vector3 (0,0.1f,0));
		this.gameObject.transform.localScale += new Vector3(0.01f,0.01f,0);
	}
}

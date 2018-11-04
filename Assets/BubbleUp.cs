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
		this.gameObject.transform.Translate (new Vector3 (0.025f,0.05f,0));
		if (this.gameObject.transform.localScale.magnitude < 2) {
			this.gameObject.transform.localScale += new Vector3 (0.01f, 0.01f, 0);
		}
	}
}

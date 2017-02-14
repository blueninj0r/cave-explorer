using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePlatform : MonoBehaviour {

	[SerializeField] public Transform platform;

	private Transform transientParent;

	// Use this for initialization
	void Start () {
		//var platform = GameObject.Find ("Platform36x01");
		// starting platform
		Destroy(transientParent);
		transientParent = Instantiate(platform, new Vector3(0,0,0), Quaternion.identity);
		// end platform (with box)

		var positions = generatePositions (30, 20);
		foreach (var position in positions) {
			var newPlatform = Instantiate(platform, position, Quaternion.identity);
			newPlatform.transform.parent = transientParent;
		}

		var finalPlatform = Instantiate(platform, new Vector3(22,22,0), Quaternion.identity);
		finalPlatform.transform.parent = transientParent;
		GameObject.Find ("levelend").transform.position = new Vector3 (22.5f, 22.5f, 0);

	}

	private List<Vector3> generatePositions(int maxX, int maxY){
		var vectors =  new List<Vector3> ();

		for (float i = 0; i < 20; i++) {
			var x = Random.Range (i, i + 3);
			var y = Random.Range (i, i + 2);
			vectors.Add(new Vector3(x, y, 0) );
		}

		return vectors;
	}
	
	// Update is called once per frame
	void Update () {
		// nothing to do here	
	}
}

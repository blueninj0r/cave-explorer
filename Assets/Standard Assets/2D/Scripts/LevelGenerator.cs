using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public class LevelGenerator : MonoBehaviour {
	[SerializeField]
	public GameObject[] platforms;
	public GameObject doorPlatform;
	public GameObject playerCharacter;

	private Dictionary<int, Transform> levelKeeper;
	private int currentLevel;

	private int LENGTH = 4;

	// Use this for initialization
	void Start () {
		currentLevel = 5;
		var connections = new List<HashSet<int>> (LENGTH);

		connections.Add(new HashSet<int>{ 1 });
		for (int i = 1; i < LENGTH; i++) {
			connections.Add( new HashSet<int> { 
				i - 1,
				i + 1
			} );
		}
		//		connections.Add( new HashSet<int>{ length - 2 });

		AddNewConnection (connections);
		AddNewConnection (connections);
		AddNewConnection (connections);

		//PrettyPrintMap (connections);

		levelKeeper = InitiateAllLevels (connections);

		ChangeLevel (0);
	}

	// Update is called once per frame
	void Update () {

	}

	void ChangeLevel(int nextLevel){
		if (nextLevel == 9) {
			SceneManager.LoadScene ("endgame");
		}  else {

			levelKeeper[currentLevel].gameObject.SetActive(false);

			Debug.Log ("level: " + nextLevel);

			var levelParent = levelKeeper [nextLevel].gameObject;
			levelParent.SetActive(true);
				
			currentLevel = nextLevel;
			ResetRobotBoy (0.5f, 5f);

//			var collider = levelParent.GetComponent<BoxCollider2D> (); //			var childColliders = levelParent.GetComponentsInChildren<BoxCollider2D> (); //			collider.enabled = true; //			childColliders.ToList().ForEach(c => c.enabled = true); 
		}

	}

	Dictionary<int, Transform> InitiateAllLevels(List<HashSet<int>> connections) {
		var levelKeeper = new Dictionary<int, Transform> ();
		Debug.Log ("IntitiateAllLevels: " + connections.Count);
		for (var i=0; i < connections.Count; i++) {
			var parent = MakeLevel (connections[i], i);
			parent.gameObject.SetActive (false);
			levelKeeper.Add (i, parent);
		}
		return levelKeeper;
	}

	Transform MakeLevel(HashSet<int> exits, int i) {
		var levelEnd = doorPlatform;

		var min = exits.Min	();
		var max = exits.Max ();

		var forwards = Instantiate(levelEnd, new Vector3 (42f, 0.5f+i, 0), Quaternion.identity); 		forwards.SendMessage("SetLevel", max);
		var levelParent = forwards.transform;

		if (min != max) {
			var back = Instantiate (levelEnd, new Vector3 (0f, 0.5f+i, 0), Quaternion.identity) as GameObject;
			back.transform.parent = levelParent;
			back.SendMessage ("SetLevel", min);
		}
		Debug.Log ("make level: " + i);
		if (i > 0) { 			var collider = forwards.GetComponent<BoxCollider2D> ();
			Debug.Log ("collider: " + collider); 			var childColliders = levelParent.GetComponentsInChildren<BoxCollider2D> (); 			collider.enabled = false; 			childColliders.ToList ().ForEach (c => c.enabled = false);
		}
			
		PlacePlatforms (new Vector3 (0f, 0.5f+i, 0), new Vector3 (42f, 0.5f+i, 0), levelParent);

		return levelParent;
	}

	void PlacePlatforms (Vector3 start, Vector3 end, Transform parent) {  		// figure out how many platforms needed 		var x_distance = end.x - start.x; 		var y_distance = end.y - start.y;  		var number_of_platforms = Math.Ceiling(x_distance / 6);  		// space platforms evenlyish - constraints need to take into account diagonal distance 		for (int i=1; i < number_of_platforms; i++) { 			var x = UnityEngine.Random.Range (i+6, i + 1); 			var y = UnityEngine.Random.Range (0.5f, 2f);  			//Choose a random tile from our array of floor tile prefabs and prepare to instantiate it. 			GameObject toInstantiate = platforms[0];  			//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject. 			GameObject instance = 				Instantiate (toInstantiate, new Vector3(i*6, y, 0), Quaternion.identity) as GameObject;  			//Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy. 			instance.transform.SetParent (parent); 		}   	}


	void ResetRobotBoy(float x, float y){
		var robotBoy = playerCharacter;
		var origin = new Vector3 (x, y, 0);
		robotBoy.transform.position = origin;
	}

	List<HashSet<int>> AddNewConnection(List<HashSet<int>> connections){
		var max = connections.Count - 1;

		var a = (int)UnityEngine.Random.Range (0, max);
		var b = (int)UnityEngine.Random.Range (0, max);

		while (a == b) {
			b = (int)UnityEngine.Random.Range (0, max);
		}

		connections [a].Add (b);
		connections [b].Add (a);

		return connections;		
	}

	void PrettyPrintMap(List<HashSet<int>> connections) {
		foreach (var list in connections) {
			var sb = new StringBuilder ();
			sb.Append ("[");
			foreach (var item in list) {
				sb.AppendFormat("{ 0}, ", item);
			}
			sb.Append("]");
			Debug.Log (sb.ToString ());
		}
	}
}


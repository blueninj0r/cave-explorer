using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public class LevelGenerator : MonoBehaviour {

	[SerializeField] public Transform platform;

	private int length = 9;

	private List<HashSet<int>> connections;
	private int currentLevel;

	private Transform transientParent;

	// Use this for initialization
	void Start () {
		currentLevel = 0;
		connections = new List<HashSet<int>> (length);

		connections.Add(new HashSet<int>{ 1 });
		for (int i = 1; i < length; i++) {
			connections.Add( new HashSet<int> { 
				i - 1,
				i + 1
			});
		}
//		connections.Add( new HashSet<int>{ length - 2 });

		AddNewConnection (connections);
		AddNewConnection (connections);
		AddNewConnection (connections);

		PrettyPrintMap (connections);

		ChangeLevel (0);

		//place initial platform
		var newPlatform = Instantiate(platform, new Vector3(0, 0.5f, 0), Quaternion.identity);
		newPlatform.transform.parent = transientParent;
	}

	// Update is called once per frame
	void Update () {
		
	}

	Transform CreateTransientParent ()
	{
		return Instantiate(platform, new Vector3(-10f,-10f,0), Quaternion.identity);
	}

	void DestroyTransientParent(){
		if (transientParent != null) {
			Debug.Log ("trying to destroy the world");
			Destroy (transientParent.gameObject);
		}
	}

	void PlacePlatforms (Vector3 start, Vector3 end) {

		// figure out how many platforms needed
		var x_distance = end.x - start.x;
		var y_distance = end.y - start.y;

		var number_of_platforms = Math.Ceiling(x_distance / 6);

		// space platforms evenlyish - constraints need to take into account diagonal distance
		for (int i=1; i < number_of_platforms; i++) {
//			var x = UnityEngine.Random.Range (i+6, i + );
			var y = UnityEngine.Random.Range (0.5f, 2f);
			var newPlatform = Instantiate(platform, new Vector3(i*6, y, 0), Quaternion.identity);
			newPlatform.transform.parent = transientParent;
		}

	}

	void ChangeLevel(int nextLevel){
		if (nextLevel == length) {
			SceneManager.LoadScene ("endgame");
		} else {

			DestroyTransientParent ();
			transientParent = CreateTransientParent ();

			Debug.Log ("level: " + nextLevel);
		

			currentLevel = nextLevel;
			var level = connections [currentLevel];

			// do work to draw the level
			MakeLevel (level);

		}
	}

	void MakeLevel(HashSet<int> exits) {
		var levelEnd = GameObject.Find ("ExitPlatform");

		var min = exits.Min	();
		var max = exits.Max ();
			
		if (min != max) {
			var back = Instantiate (levelEnd, new Vector3 (0f, 0.5f, 0), Quaternion.identity);
			back.transform.parent = transientParent;
			back.SendMessage ("SetLevel", min);
		}


		var forwards = Instantiate(levelEnd, new Vector3 (42f, 0.5f, 0), Quaternion.identity);
		forwards.transform.parent = transientParent;
		forwards.SendMessage("SetLevel", max);

		PlacePlatforms (new Vector3 (0f, 0.5f, 0), new Vector3 (42f, 0.5f, 0));
		ResetRobotBoy (2.6f, 1.5f);
	}


	void ResetRobotBoy(float x, float y){
		var robotBoy = GameObject.Find ("CharacterRobotBoy");
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
				sb.AppendFormat("{0}, ", item);
			}
			sb.Append("]");
			Debug.Log (sb.ToString ());
		}
	}
}

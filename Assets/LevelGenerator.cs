using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.SceneManagement;

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
	}

	// Update is called once per frame
	void Update () {
		
	}

	Transform CreateTransientParent ()
	{
		return Instantiate(platform, new Vector3(0,0,0), Quaternion.identity);
	}

	void DestroyTransientParent(){
		if (transientParent != null) {
			Debug.Log ("trying to destroy the world");
			Destroy (transientParent.gameObject);
		}
	}

	void PlacePlatforms () {
		//var platform = GameObject.Find ("Platform36x01");
		// starting platform

		// end platform (with box)

		var positions = generatePositions (30, 20);
		foreach (var position in positions) {
			var newPlatform = Instantiate(platform, position, Quaternion.identity);
			newPlatform.transform.parent = transientParent;
		}

		var finalPlatform = Instantiate(platform, new Vector3(22,22,0), Quaternion.identity);
		finalPlatform.transform.parent = transientParent;

	}

	private List<Vector3> generatePositions(int maxX, int maxY){
		var vectors =  new List<Vector3> ();

		for (float i = 0; i < 5; i++) {
			var x = Random.Range (i+2, i + 4);
			var y = Random.Range (i, i + 2);
			vectors.Add(new Vector3(x, y, 0) );
		}

		return vectors;
	}

	void ChangeLevel(int nextLevel){
		if (nextLevel == length) {
			SceneManager.LoadScene ("endgame");
		} else {

			DestroyTransientParent ();
			transientParent = CreateTransientParent ();

			Debug.Log ("level: " + nextLevel);

			ResetRobotBoy ();

			currentLevel = nextLevel;
			var level = connections [currentLevel];
			// do work to draw the level	
			var levelEnd = GameObject.Find ("ExitPlatform");
			foreach (var exit in level) {
				var thing = Instantiate(levelEnd, new Vector3 ((exit*2) + 3f, 0.5f, 0), Quaternion.identity);
				thing.transform.parent = transientParent;
				thing.SendMessage("SetLevel", exit);
			}

			//PlacePlatforms ();
		}
	}


	void ResetRobotBoy(){
		var robotBoy = GameObject.Find ("CharacterRobotBoy");
		var origin = new Vector3 (0, 0, 0);
		robotBoy.transform.position = origin;
	}

	List<HashSet<int>> AddNewConnection(List<HashSet<int>> connections){
		var max = connections.Count - 1;

		var a = (int)Random.Range (0, max);
		var b = (int)Random.Range (0, max);

		while (a == b) {
			b = (int)Random.Range (0, max);
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

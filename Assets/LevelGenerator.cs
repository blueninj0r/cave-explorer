using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class LevelGenerator : MonoBehaviour {
	
	private int length = 9;

	private List<HashSet<int>> connections;
	private int currentLevel;

	// Use this for initialization
	void Start () {
		currentLevel = 0;
		connections = new List<HashSet<int>> (length);

		connections.Add(new HashSet<int>{ 1 });
		for (int i = 1; i < length-1; i++) {
			connections.Add( new HashSet<int> { 
				i - 1,
				i + 1
			});
		}
		connections.Add( new HashSet<int>{ length - 2 });

		AddNewConnection (connections);
		AddNewConnection (connections);
		AddNewConnection (connections);

		PrettyPrintMap (connections);



	}

	// Update is called once per frame
	void Update () {
		
	}

	void ChangeLevel(int nextLevel){
		currentLevel = nextLevel;
		var level = connections [currentLevel];
		// do work to draw the level
		var levelEnd = GameObject.Find ("levelend");
		foreach (var exit in level) {
			var thing = Instantiate(levelEnd, new Vector3 (1.5f, 1.5f, 0), Quaternion.identity);
			thing.SendMessage("SetLevel", exit);
		}
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

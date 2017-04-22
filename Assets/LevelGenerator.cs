﻿using System.Collections; using System.Collections.Generic; using UnityEngine; using System.Text; using UnityEngine.SceneManagement; using System.Linq; using System;  public class LevelGenerator : MonoBehaviour { 	[SerializeField] 	public GameObject[] platforms; 	public GameObject doorPlatform; 	public GameObject playerCharacter;  	private int length = 9;  	private int currentLevel; 	private Dictionary<int, Transform> levels;   	// Use this for initialization 	void Start () { 		currentLevel = 0; 		var connections = new List<HashSet<int>> (length);  		connections.Add(new HashSet<int>{ 1 }); 		for (int i = 1; i < length; i++) { 			connections.Add( new HashSet<int> {  				i - 1, 				i + 1 			} ); 		} 		//		connections.Add( new HashSet<int>{ length - 2 });  		AddNewConnection (connections); 		AddNewConnection (connections); 		AddNewConnection (connections);  //		PrettyPrintMap (connections);  		Debug.Log ("hiiiiiiiiiiiii");  		levels = InitialiseLevels (connections);  		ChangeLevel (0); 		}  	// Update is called once per frame 	void Update () {  	}  	Dictionary<int, Transform> InitialiseLevels(List<HashSet<int>> connections){ 		var levels = new Dictionary<int, Transform> (); 		for(var i=0; i < connections.Count; i++){ 			levels.Add(i, MakeLevel (connections [i])); 		}  		return levels; 	}  	void PlacePlatforms (Vector3 start, Vector3 end, Transform levelParent) {  		// figure out how many platforms needed 		var x_distance = end.x - start.x; 		var y_distance = end.y - start.y;  		var number_of_platforms = Math.Ceiling(x_distance / 6);  		// space platforms evenlyish - constraints need to take into account diagonal distance 		for (int i=1; i < number_of_platforms; i++) { 			var x = UnityEngine.Random.Range (i+6, i + 1); 			var y = UnityEngine.Random.Range (0.5f, 2f);  			//Choose a random tile from our array of floor tile prefabs and prepare to instantiate it. 			GameObject toInstantiate = platforms[0];  			//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject. 			GameObject instance = 				Instantiate (toInstantiate, new Vector3(i*6, y, 0), Quaternion.identity) as GameObject;  			//Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy. 			instance.transform.SetParent (levelParent); 		}   	}  	void ChangeLevel(int nextLevel){ 		if (nextLevel == length) { 			SceneManager.LoadScene ("endgame"); 		}  else {  			levels [currentLevel].gameObject.SetActive (false); 			levels [nextLevel].gameObject.SetActive (true); 			Debug.Log ("level: " + nextLevel);   			currentLevel = nextLevel;  			ResetRobotBoy (2.6f, 1.5f); 		} 	}  	Transform MakeLevel(HashSet<int> exits) { 		var levelEnd = doorPlatform;  		var min = exits.Min	(); 		var max = exits.Max ();  		var forwards = Instantiate(levelEnd, new Vector3 (42f, 0.5f, 0), Quaternion.identity); 		forwards.SendMessage("SetLevel", max); 		var levelParent = forwards.transform;  		if (min != max) { 			var back = Instantiate (levelEnd, new Vector3 (0f, 0.5f, 0), Quaternion.identity) as GameObject; 			back.transform.SetParent(levelParent); 			back.SendMessage ("SetLevel", min); 		}   		PlacePlatforms (new Vector3 (0f, 0.5f, 0), new Vector3 (42f, 0.5f, 0), levelParent); 		levelParent.gameObject.SetActive (false); 		return levelParent; 	}   	void ResetRobotBoy(float x, float y){ 		var robotBoy = playerCharacter; 		var origin = new Vector3 (x, y, 0); 		robotBoy.transform.position = origin; 	}  	List<HashSet<int>> AddNewConnection(List<HashSet<int>> connections){ 		var max = connections.Count - 1;  		var a = (int)UnityEngine.Random.Range (0, max); 		var b = (int)UnityEngine.Random.Range (0, max);  		while (a == b) { 			b = (int)UnityEngine.Random.Range (0, max); 		}  		connections [a].Add (b); 		connections [b].Add (a);  		return connections;		 	}  	void PrettyPrintMap(List<HashSet<int>> connections) { 		foreach (var list in connections) { 			var sb = new StringBuilder (); 			sb.Append ("["); 			foreach (var item in list) { 				sb.AppendFormat("{0}, ", item); 			} 			sb.Append("]"); 			Debug.Log (sb.ToString ()); 		} 	} }  
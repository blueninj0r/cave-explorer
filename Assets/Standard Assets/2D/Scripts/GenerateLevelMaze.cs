using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevelMaze : MonoBehaviour {

	public enum Direction {
		North,
		South,
		East,
		West
	}

	struct Cell {
		public bool North;
		public bool South;
		public bool East;
		public bool West;
	}

	private List<List<Cell>> grid;
	private Vector2 startingPoint;

	// Use this for initialization
	void Start () {
		var startX = (int)Random.Range (0, 8);
		var startY = (int)Random.Range (0, 8);

		startingPoint = new Vector2 (startX, startY);

		grid = new List<List<Cell>>();

	}

	bool HasCellBeenVisited(Cell cell){
		return cell.North || cell.South || cell.East || cell.West;
	}

	void ChooseNextCell(Cell currentCell, Vector2 location) {
		var direction = Random.Range (0, 3);

	}

	void PositionBasedOnDirection(Vector2 location, int direction) {
		if (direction == 0) {
			location.y = location.y + 1;
		} else if (direction == 1) {
			location.y = location.y - 1;
		} else if (direction == 2) {
			location.x = location.x + 1;
		} else if (direction == 3) {
			location.x = location.x - 1;
		}
		//return location;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void PrintToConsole(){
		Debug.Log ("Hi");

	}

}

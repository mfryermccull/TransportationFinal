using UnityEngine;
using System.Collections;

public class GameControllerScript : MonoBehaviour
{
	public GameObject cubePrefab;
	private GameObject[,] allCubes;
	public GameObject aCube;
	public Airplane airplane;
	int gridWidth = 16;
	int gridHeight = 9;
	int depotX = 15;
	int depotY = 0;
	int airplaneStartX = 0;
	int airplaneStartY = 8;
	int score = 0;
	//each turn takes 1.5 seconds
	float turnLength = 1.5f;
	float timeToAct;




	// If the player clicks the sky and there is an active airplane,
	//   the airplane teleports to that location
	public void ProcessClickedCube (GameObject clickedCube, int x, int y)
	{
		// If the player clicks an inactive airplane, it should highlight
		if (x == airplane.x && y == airplane.y && airplane.active == false) {

			airplane.active = true;
			clickedCube.GetComponent<Renderer> ().material.color = Color.yellow;

			// If the player clicks an active airplane, it should unhighlight
		} else if (x == airplane.x && y == airplane.y && airplane.active) {

			airplane.active = false;
			clickedCube.GetComponent<Renderer> ().material.color = Color.red;

			// If the player clicks the sky and there isn’t an active airplane, 
			//   nothing happens.
		} else if (airplane.active && (x != airplane.x || y != airplane.y)) {

			airplane.targetX = x;
			airplane.targetY = y;
		}

	}
	//rules need to be set before game start

	// Use this for initialization
	void Start ()
	{

		timeToAct = turnLength;

		allCubes = new GameObject[gridWidth, gridHeight];
		for (int x = 0; x < gridWidth; x++) {
			for (int y = 0; y < gridHeight; y++) {
				allCubes [x, y] = (GameObject)Instantiate (aCube, new Vector3 (x * 2 - 14, y * 2 - 8, 10), Quaternion.identity);
				allCubes [x, y].GetComponent<CubeBehaviour> ().x = x;
				allCubes [x, y].GetComponent<CubeBehaviour> ().y = y;
			}
		}

		foreach (GameObject oneCube in allCubes) {
			oneCube.GetComponent<Renderer> ().material.color = Color.white;
		}
		
		airplane = new Airplane ();

		airplane.targetX = airplaneStartX;
		airplane.targetY = airplaneStartY;
		airplane.x = airplaneStartX;
		airplane.y = airplaneStartY;



		allCubes [airplaneStartX, airplaneStartY].GetComponent<Renderer> ().material.color = Color.red;

		if (airplane.x == airplaneStartX && airplane.y == airplaneStartY) {
			airplane.cargo = Mathf.Min (airplane.cargo + 10, airplane.capacity);
		}

		allCubes [depotX, depotY].GetComponent<Renderer> ().material.color = Color.black;
	}
	
	void MoveAirplane ()
	{

		int nextX = airplane.x;
		int nextY = airplane.y;

			if (airplane.targetX > airplane.x) {
				nextX++;
			} else if (airplane.targetX < airplane.x) {
				nextX--;
			}
			
			if (airplane.targetY > airplane.y) {
				nextY++;
			} else if (airplane.targetY < airplane.y) {
				nextY--;
			
		}
		// Set the old cube to black if it's the depot
		if (airplane.x == depotX && airplane.y == depotY) {
			allCubes [airplane.x, airplane.y].GetComponent<Renderer> ().material.color = Color.black;
		}
		// otherwise, set the old cube to white
		else {
			allCubes [airplane.x, airplane.y].GetComponent<Renderer> ().material.color = Color.white;
		}
		
		// Set the new cube to yellow if the airplane is still active
		if (airplane.active) {
			allCubes [nextX, nextY].GetComponent<Renderer> ().material.color = Color.yellow;
		}
		// otherwise the airplane is deactive and red
		else {
			allCubes [nextX, nextY].GetComponent<Renderer> ().material.color = Color.red;
		}

		// Update the airplane to be in the new location
		airplane.x = nextX;
		airplane.y = nextY;
	}
	// Update is called once per frame
	void Update ()
	{

		if (Time.time > timeToAct) {
				MoveAirplane ();

				timeToAct += turnLength;
			}
		if (airplane.x == depotX && airplane.y == depotY) {
			score += airplane.cargo;
			airplane.cargo = 0;
		}

		
	
		//check if airplane is in starting position
		//if so, add 10 tons of cargo, but not over max capacity of 90
		//show current cargo amount on screen
		//1 point per each ton of cargo successfully delivered to bottom black cube

	}
}





using UnityEngine;
using System.Collections;

public class CubeBehaviour : MonoBehaviour {
	GameControllerScript aGameController;
	public int x, y;

	// Use this for initialization
	void Start () {
		aGameController = GameObject.Find("GameControllerObject").GetComponent<GameControllerScript>();

	}
	void OnMouseDown () {
		aGameController.ProcessClickedCube(this.gameObject, x, y);


	}

	// Update is called once per frame
	void Update () {

	}
}

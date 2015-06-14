using UnityEngine;
using System.Collections;

public class StartMenuScript : MonoBehaviour {
	
	private bool xPressed = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown ("X") && !xPressed) {
			GameManager.NextLevel();
			xPressed = true;
		}
	
	}
}

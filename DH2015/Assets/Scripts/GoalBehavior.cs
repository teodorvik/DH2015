using UnityEngine;
using System.Collections;

public class GoalBehavior : MonoBehaviour {
	public int colorId;

	// Use this for initialization
	void Start () {
		Color color = GameManager.colors [colorId];
		color.a = 0.5f;
		GetComponent<SpriteRenderer> ().color = color;
	}

	void OnTriggerEnter2D() {
		//GameManager.EnterGoal ();
		Color color = GameManager.colors [colorId];
		color.a = 1f;
		GetComponent<SpriteRenderer> ().color = color;
	}

	void OnTriggerExit2D() {
		//GameManager.ExitGoal ();
		Color color = GameManager.colors [colorId];
		color.a = 0.5f;
		GetComponent<SpriteRenderer> ().color = color;
	}
	
	
}

using UnityEngine;
using System.Collections;

public class GoalBehavior : MonoBehaviour {
	public int colorId;
	private Color color;
	// Use this for initialization
	void Start () {
		color = GameManager.colors [colorId];
		color.a = 0.5f;
		GetComponent<SpriteRenderer> ().color = color;
	}

	void OnTriggerEnter2D(Collider2D other) {
		//GameManager.EnterGoal ();

		if (other.GetComponent<PlayerBehaviour> ().colorId == colorId) {
			color.a = 1f;
			GameManager.EnterGoal();
			GetComponent<SpriteRenderer> ().color = color;
			
		} 
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.GetComponent<PlayerBehaviour> ().colorId == colorId) {
			color.a = 0.5f;
			GameManager.ExitGoal();
			GetComponent<SpriteRenderer> ().color = color;
			
		} 
	}

}

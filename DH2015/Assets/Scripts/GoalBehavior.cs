using UnityEngine;
using System.Collections;

public class GoalBehavior : MonoBehaviour {
	private Color color;

	public enum colorNames {
		Red = 1,
		Green = 2,
		Blue = 4,
		Yellow = 8
	};
	
	public colorNames colorId;
	// Use this for initialization
	void Start () {
		color = GameManager.colors [(int)colorId];
		color.a = 0.5f;
		GetComponent<SpriteRenderer> ().color = color;
	}

	void OnTriggerEnter2D(Collider2D other) {
		//GameManager.EnterGoal ();

		if ((int)other.GetComponent<PlayerBehaviour> ().colorId == (int)colorId) {
			color.a = 1f;
			GameManager.EnterGoal();
			GetComponent<SpriteRenderer> ().color = color;
			
		} 
	}

	void OnTriggerExit2D(Collider2D other) {
		if ((int)other.GetComponent<PlayerBehaviour> ().colorId == (int)colorId) {
			color.a = 0.5f;
			GameManager.ExitGoal();
			GetComponent<SpriteRenderer> ().color = color;
			
		} 
	}

}

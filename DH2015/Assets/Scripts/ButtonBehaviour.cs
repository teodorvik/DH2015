using UnityEngine;
using System.Collections;

public class ButtonBehaviour : MonoBehaviour {
	
	public GameObject field;
	public int newColorId;
	public Sprite up, down;

	private SpriteRenderer renderer;
	private int oldColorId;
	private FieldManager fieldManager;
	// Use this for initialization
	void Start () {

		renderer = GetComponent<SpriteRenderer> ();
		fieldManager = field.GetComponent<FieldManager> ();

		oldColorId = fieldManager.colorId;

	}
	
	void OnTriggerStay2D(Collider2D other) {
		//GameManager.EnterGoal ();
		
		renderer.sprite = down;
		fieldManager.colorId = newColorId;
		Color c = GameManager.colors [newColorId];
		c.a = 0.5f;
		fieldManager.GetComponent<SpriteRenderer>().color = c;
	}
	
	void OnTriggerExit2D(Collider2D other) {

		renderer.sprite = up;
		fieldManager.colorId = oldColorId;
		Color c = GameManager.colors [oldColorId];
		c.a = 0.5f;
		fieldManager.GetComponent<SpriteRenderer>().color = c;
		
	}
}

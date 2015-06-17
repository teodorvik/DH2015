using UnityEngine;
using System.Collections;

public class ButtonBehaviour : MonoBehaviour {
	public GameManager.colorNames colorId;
	
	public GameObject field;
	private int newColorId;
	public Sprite up, down;
	public bool state = false;
	private bool oldState;

	private AudioSource audio;
	private SpriteRenderer renderer;
	private int oldColorId;
	private FieldManager fieldManager;
	// Use this for initialization
	void Start () {

		renderer = GetComponent<SpriteRenderer> ();
		fieldManager = field.GetComponent<FieldManager> ();

//		oldColorId = (int)fieldManager.colorId;

		oldState = state;

		audio = GetComponent<AudioSource> ();

		newColorId = (int)colorId;

	}
	
	void OnTriggerStay2D(Collider2D other) {
		if (state == false) {
			renderer.sprite = down;

//			fieldManager.colorId = ;
			Color c = GameManager.colors [newColorId];
			c.a = 0.5f;
			fieldManager.GetComponent<SpriteRenderer>().color = c;

			state = true;

			audio.Play();
		}
	}
	
	void OnTriggerExit2D(Collider2D other) {

		renderer.sprite = up;
//		fieldManager.colorId = FieldManager.colorNames[newColorId];
		Color c = GameManager.colors [oldColorId];
		c.a = 0.5f;
		fieldManager.GetComponent<SpriteRenderer>().color = c;
		state = false;

		audio.pitch = 1.2f;
		audio.Play();
	}
}

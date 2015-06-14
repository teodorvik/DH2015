using UnityEngine;
using System.Collections;

public class ButtonBehaviour : MonoBehaviour {
	
	public GameObject field;
	public int newColorId;
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

		oldColorId = fieldManager.colorId;

		oldState = state;

		audio = GetComponent<AudioSource> ();

	}
	
	void OnTriggerStay2D(Collider2D other) {
		if (state == false) {
			renderer.sprite = down;
			fieldManager.colorId = newColorId;
			Color c = GameManager.colors [newColorId];
			c.a = 0.5f;
			fieldManager.GetComponent<SpriteRenderer>().color = c;

			state = true;

			audio.Play();
		}
	}
	
	void OnTriggerExit2D(Collider2D other) {

		renderer.sprite = up;
		fieldManager.colorId = oldColorId;
		Color c = GameManager.colors [oldColorId];
		c.a = 0.5f;
		fieldManager.GetComponent<SpriteRenderer>().color = c;
		state = false;

		audio.pitch = 1.2f;
		audio.Play();
	}
}

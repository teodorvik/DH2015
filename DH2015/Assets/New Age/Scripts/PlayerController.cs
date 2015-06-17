using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	public Colors.names color;
	public List<Colors.names> colors = new List<Colors.names>();
	public bool inField = false;
	public float gravity;

	private SpriteRenderer sRenderer;

	void Start() {
		sRenderer = GetComponent<SpriteRenderer>();

		colors.Add(color);		
		sRenderer.color = Colors.Get(colors);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			var player = other.GetComponent<PlayerController>();
			var pColor = player.color;

			colors.Add(pColor);
			sRenderer.color = Colors.Get(colors);
		}
	}
	
	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			var player = other.GetComponent<PlayerController>();
			var pColor = player.color;
			
			colors.Remove(pColor);
			sRenderer.color = Colors.Get(colors);
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.tag == "Player") {
			var pos = other.transform.position;
			var dir = transform.position - pos;

			other.attachedRigidbody.AddForce(dir * gravity);
		}
	}
}

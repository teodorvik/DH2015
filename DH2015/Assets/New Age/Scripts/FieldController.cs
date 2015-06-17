using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldController : MonoBehaviour {
	public Colors.names color;
	public float force;
	
	private SpriteRenderer sRenderer;
	
	private Dictionary<GameObject, Vector3> players =
		new Dictionary<GameObject, Vector3>();
	
	// Use this for initialization
	void Start () {
		sRenderer = GetComponent<SpriteRenderer>();
		var _color = Colors.Get(color);
		_color.a = 0.5f;
		sRenderer.color = _color;
	}

	void FixedUpdate() {
		foreach (var entry in players) {
			var player		= entry.Key;
			var controller 	= player.GetComponent<PlayerController>();

			if (Colors.Mix(controller.colors) != color) {
				var body = player.GetComponent<Rigidbody2D>();
				var dir  = entry.Value - transform.position;

				body.AddForce(dir * force);
			}
		}
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			players.Add(other.gameObject, other.transform.position);
			other.gameObject
				.GetComponent<PlayerController>()
				.inField = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			players.Remove(other.gameObject);
			other.gameObject
				.GetComponent<PlayerController>()
				.inField = false;			
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CursorController : MonoBehaviour {
	public Color active, inactive;
	public float pull, push, tapTime;

	private SpriteRenderer sprite;
	private Vector3 mousePos;
	private float downTime, radius;

	private List<GameObject> players =
		new List<GameObject>();

	// Use this for initialization
	void Start () {
		Cursor.visible = false;	
		sprite = GetComponent<SpriteRenderer>();
		radius = GetComponent<CircleCollider2D>().radius;
	}
	
	// Update is called once per frame
	void Update () {
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePos.z = 0;
		transform.position = mousePos;

		if (Input.GetMouseButtonDown(0)) {
			sprite.color = active;
			downTime = Time.time;
		}

		if (Input.GetMouseButtonUp(0)) {
			sprite.color = inactive;
			var delta = Time.time - downTime;

			if (delta < tapTime && players.Count > 1) {
				var iAngle = 360f / players.Count;
				var rAngle = 360f * Random.value;

				var i = 0;
				foreach (var player in players) {
					var pController = player.GetComponent<PlayerController>();

					if (!pController.inField) {
						var rBody = player.GetComponent<Rigidbody2D>();					
						
						var rad = (iAngle * i++ + rAngle) * Mathf.Deg2Rad;
						var dir = new Vector2(Mathf.Sin(rad), Mathf.Cos(rad));
						
						rBody.AddForce(dir * push, ForceMode2D.Impulse);
					}

				}
			}
		}
	}

	void FixedUpdate() {
		if (Input.GetMouseButton(0)) {
			foreach (var player in players) {
				var pController = player.GetComponent<PlayerController>();
				var rBody 		= player.GetComponent<Rigidbody2D>();
				
				var pos = player.transform.position;
				var dir = transform.position - pos;
				
				rBody.AddForce(dir * pull);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			players.Add(other.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			players.Remove(other.gameObject);
		}
	}
}

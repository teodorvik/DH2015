using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseInput : MonoBehaviour {

	public float range;
	public float pull;
	public float push;
	public float tapTime;

	private float downTime;
	private List<GameObject> players = new List<GameObject>();

	private GameObject cursor;

	void Start() {
		cursor = GameObject.Find("Cursor");

	}

	void Update() {
		if (Input.GetMouseButtonDown (0)) {
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			GameObject[] _players = GameObject.FindGameObjectsWithTag("Player");

			downTime = Time.time;
			mousePos.z = 0;

			foreach (var player in _players) {
				float distance = Vector3.Distance(mousePos, player.transform.position);

				if (distance < range) {
					players.Add(player);
				}
			}


		}
		
		if (Input.GetMouseButtonUp (0)) {
			float delta = Time.time - downTime;
			
			if (delta < tapTime) {
				float angle = 360 / Mathf.Max(players.Count, 1);
				float rAngle = Random.value * 360;

				for(var i = 0; i < players.Count; i ++) {
					var player = players[i];
					var pController = player.GetComponent<PlayerController>();
			
					if (!pController.inField) {
						var rad = (angle * i + rAngle) * Mathf.Deg2Rad;
						var direction = new Vector3(Mathf.Sin(rad), Mathf.Cos(rad));

						player.GetComponent<Rigidbody2D>()
							.AddForce(direction * push);
					}
				}			
			}

			players.Clear();
		}
	}

	void FixedUpdate () {
		if (Input.GetMouseButton(0)) {
			float delta = Time.time - downTime;
			
			if (delta > tapTime) {
				Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

				mousePos.z = 0;

				foreach (var player in players) {
					float distance = Vector3.Distance(mousePos, player.transform.position);
					
					if (distance < range) {
						Vector3 direction = mousePos - player.transform.position;
						var distFactor = 1 - distance/range;

						player.GetComponent<Rigidbody2D>()
							.AddForce(direction * pull * distFactor);
					}
				}
			}
		}	
	}
}

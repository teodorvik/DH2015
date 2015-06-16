using UnityEngine;
using System.Collections;

public class MouseInput : MonoBehaviour {

	public float range;
	public float strength;

	private float downTime;

	// Use this for initialization
	void Start () {
	
	}

	void Update() {
		if (Input.GetMouseButtonDown (0)) {
			downTime = Time.time;
		}
		
		if (Input.GetMouseButtonUp (0)) {
			float delta = Time.time - downTime;
			
			Debug.Log(delta);
			
			if (delta < 0.1f) {
				
				Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
				
				mousePos.z = 0;

				float angle = 360 / players.Length;

				for (int i = 0; i < players.Length; i++) {
					GameObject player = players[i];
					float distance = Vector3.Distance(mousePos, player.transform.position);
					bool illigal = player.GetComponent<PlayerBehaviour>().illigal;
					
					if (distance < range && !illigal) {

						var rad = angle * i * Mathf.Deg2Rad;
						var direction = new Vector3(Mathf.Sin(rad), Mathf.Cos(rad));
						float distFactor = 1 - distance / range;

						player.GetComponent<Rigidbody2D>().AddForce(direction * strength * 5 * distFactor);
					}

				}				
			}
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {


		if (Input.GetMouseButton(0)) {
			float delta = Time.time - downTime;
			
			if (delta > 0.1f) {

				Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

				mousePos.z = 0;

				foreach (GameObject player in players) {
					float distance = Vector3.Distance(mousePos, player.transform.position);

					bool illigal = player.GetComponent<PlayerBehaviour>().illigal;

					if (distance < range && !illigal) {
						Vector3 direction = mousePos - player.transform.position;
						float distFactor = 1 - distance / range;
						player.GetComponent<Rigidbody2D>().AddForce(direction * strength * distFactor);
					}
				}
			}
		}	
	}
}

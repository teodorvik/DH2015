using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerBehaviour : MonoBehaviour {
	public float speed;
	public char player;
	public int colorId;
	public int currentColorId;
	public GameObject Blood;

	private SpriteRenderer renderer;

	private List<int> collisions = new List<int>();
	private List<Vector2> history = new List<Vector2>();
	
	private Dictionary<int, Color> colors;
	
	void Start() {
		colors = GameManager.colors;
		renderer = GetComponent<SpriteRenderer> ();
		collisions.Add (colorId);
		currentColorId = collisions.Sum ();
		//renderer.color = colors[currentColorId];
		Vector2 pos = new Vector2 (transform.position.x, transform.position.y);
		history.Add (pos);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == 8) { // Layer 8: Players
			int oColorId = other.GetComponent<PlayerBehaviour>().colorId;
			collisions.Add (oColorId);
			currentColorId = collisions.Sum ();
			//renderer.color = colors[currentColorId];
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.layer == 8) { // Layer 8: Players
			int oColorId = other.GetComponent<PlayerBehaviour>().colorId;
			collisions.Remove (oColorId);
			currentColorId = collisions.Sum ();
			//renderer.color = colors[currentColorId];
		}
	}

	void Update() {
		Vector2 pos = new Vector2 (transform.position.x, transform.position.y);
		Vector2 oldPos = history[history.Count - 1];
		float delta = Vector2.Distance (pos, oldPos);

		if (delta > 0.1f) {
			history.Add (pos);
			print(pos);
		}
	}

	void FixedUpdate () {
		float x = Input.GetAxis("X_Player" + player);
		float y = Input.GetAxis("Y_Player" + player);

		Vector3 force = new Vector3(x * speed, y * speed);

		GetComponent<Rigidbody2D>().AddForce(force);
	}

	public void Kill() {
		print ("A Player Died");

		var color = colors [colorId];
		color.a = 0.5f;

		Blood.GetComponent<ParticleSystem> ().startColor = color;
		Instantiate (Blood, transform.position, transform.rotation);

		transform.position = history [0];
	}
}

/*
 * Red
 * Green
 * Blue
 * Yellow
 * 
 * Red + Green = Brown
 * Red + Blue = Purple
 * Red + Yellow = Orange
 * 
 * Green + Blue = Turqoise  
 * Green + Yellow = Lime green
 * 
 * Blue + Yellow = Green
 * 
 * Red + Green + Blue = Brown
 * Red + Green + Yellow = Brown
 * 
 * Red + Blue + Green = Brown
 * Red + Blue + Yellow = Brown
 * 
 * Green + Blue + Yellow = Green
 * 
 * Red + Green + Blue + Yellow = Brown
 * 
 */

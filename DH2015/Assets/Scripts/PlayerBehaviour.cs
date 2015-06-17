using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerBehaviour : MonoBehaviour {
	public float speed;
	public char player;
//	public int colorId;
	public int currentColorId;
	public bool illigal;
	private SpriteRenderer renderer;

	private List<int> collisions = new List<int>();
	private List<Vector2> history = new List<Vector2>();
	
	private Dictionary<int, Color> colors;


	public enum colorNames {
		Red = 1,
		Green = 2,
		Blue = 4,
		Yellow = 8
	};

	public colorNames colorId;
	
	void Start() {
		colors = GameManager.colors;
		renderer = GetComponent<SpriteRenderer> ();
		collisions.Add ((int)colorId);
		currentColorId = collisions.Sum ();
		renderer.color = colors[currentColorId];
		Vector2 pos = new Vector2 (transform.position.x, transform.position.y);
		history.Add (pos);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") { // Layer 8: Players
			int oColorId = (int)other.GetComponent<PlayerBehaviour>().colorId;
			collisions.Add (oColorId);
			currentColorId = collisions.Sum ();
			renderer.color = colors[currentColorId];
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Player") { // Layer 8: Players
			int oColorId = (int)other.GetComponent<PlayerBehaviour>().colorId;
			collisions.Remove (oColorId);
			currentColorId = collisions.Sum ();
			renderer.color = colors[currentColorId];
		}
	}

	void FixedUpdate () {
		float x = Input.GetAxis("X_Player" + player);
		float y = Input.GetAxis("Y_Player" + player);

		Vector3 force = new Vector3(x * speed, y * speed);

		GetComponent<Rigidbody2D>().AddForce(force);
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

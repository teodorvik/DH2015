using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerBehaviour : MonoBehaviour {
	public float speed;
	public char player;
	public int colorId;

	private SpriteRenderer renderer;

	private List<int> collisions = new List<int>();

	private Dictionary<int, Color> colors = new Dictionary<int, Color>() {
		{1, new Color(1f,0f,0f)}, // Red
		{2, new Color(0f,1f,0f)}, // Green
		{4, new Color(0f,0f,1f)}, // Blue
		{8, new Color(1f,1f,0f)}, // Yellow

		{3, new Color(1f,1f,1f)}, // Red + Green = Brown
		{5, new Color(1f,0f,1f)}, // Red + Blue = Purple
		{9, new Color(1f,1f,1f)}, // Red + Yellow = Orange

		{6, new Color(0f,1f,1f)},  // Green + Blue = Turqoise  
		{10, new Color(1f,1f,1f)}, // Green + Yellow = Lime green

		{12, new Color(0f,1f,0f)}, // Blue + Yellow = Green

		{7, new Color(1f,1f,1f)},  // Red + Green + Blue = Brown
		{11, new Color(1f,1f,1f)}, // Red + Green + Yellow = Brown

		{14, new Color(1f,1f,1f)}, // Green + Blue + Yellow = Green

		{15, new Color(1f,1f,1f)} // Red + Green + Blue + Yellow = Brown
	};
	
	void Start() {
		renderer = GetComponent<SpriteRenderer> ();
		collisions.Add (colorId);
		renderer.color = colors[collisions.Sum()];
	}

	void OnTriggerEnter2D(Collider2D other) {
		int oColorId = other.GetComponent<PlayerBehaviour>().colorId;
		collisions.Add (oColorId);
		renderer.color = colors[collisions.Sum()];
	}

	void OnTriggerExit2D(Collider2D other) {
		int oColorId = other.GetComponent<PlayerBehaviour>().colorId;
		collisions.Remove (oColorId);
		renderer.color = colors[collisions.Sum()];
	}

	void FixedUpdate () {
		float x = Input.GetAxis("X_Player" + player);
		float y = Input.GetAxis("Y_Player" + player);

		Vector3 force = new Vector3(x * speed, y * speed);

		//print (force);
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

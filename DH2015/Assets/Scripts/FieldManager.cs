using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldManager : MonoBehaviour {

	public GameManager.colorNames color;
	public float strength;

	private Dictionary<int, Color> colors;
	private SpriteRenderer renderer;
	private Dictionary<int, Vector3> aliens = new Dictionary<int, Vector3>();

	
	// Use this for initialization
	void Start () {
		colors = GameManager.colors;
		renderer = GetComponent<SpriteRenderer> ();
//		Color color = colors [colorId];
//		color.a = 0.5f;
//		renderer.color = color;
//		Debug.Log ((int)colorNames.Orange);
	}


	void OnTriggerEnter2D(Collider2D other) {
		int id = other.GetInstanceID ();

		if (aliens.ContainsKey (id)) {
			aliens [id] = other.transform.position;
		} else {
			aliens.Add(id, other.transform.position);
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.layer == 8) { // Layer 8: Players
			PlayerBehaviour Player = other.GetComponent<PlayerBehaviour>();
			int pColorId = Player.currentColorId;
			switch (pColorId) {
			case 12:
			case 14:
				pColorId = 2;
				break;
			case 7:
			case 11:
			case 15:
				pColorId = 3;
				break;
				
			}
//			if (pColorId != colorId) {
//				int id = other.GetInstanceID ();
//
//				Vector3 force = aliens[id] - transform.position;
//				float distance = Vector3.Distance(aliens[id], transform.position);
//
//				other.attachedRigidbody.AddForce(force * Mathf.Pow(strength, distance));
//				other.GetComponent<PlayerBehaviour>().illigal = true;
//			} else {
//				other.GetComponent<PlayerBehaviour>().illigal = false;
//			}
		}

	}

	void OnTriggerExit2D(Collider2D other) {
		other.gameObject
			.GetComponent<PlayerBehaviour>().illigal = false;
	}
}


//{1, Red
//{2, Green
//{3, Brown
//{4, Blue
//{5, Purple
//{6, Turqoise 

//{7, Brown

//{8, Yellow
//{9, Orange
//{10, Lime green

//{11, Brown
//{12, Green
//{13, Brown
//{14, Green
//{15, Brown





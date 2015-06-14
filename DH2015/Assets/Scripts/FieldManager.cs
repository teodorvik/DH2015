using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldManager : MonoBehaviour {
	public int colorId;
	public float strength;

	private Dictionary<int, Color> colors;
	private SpriteRenderer renderer;

	private List<KeyValuePair<int, Vector3>> illegalEntries = new List<KeyValuePair<int, Vector3>>();
	private Dictionary<int, Vector3> aliens = new Dictionary<int, Vector3>();
	
	// Use this for initialization
	void Start () {
		colors = GameManager.colors;
		renderer = GetComponent<SpriteRenderer> ();
		Color color = colors [colorId];
		color.a = 0.5f;
		renderer.color = color;
	}


	void OnTriggerEnter2D(Collider2D other) {
		Vector3 force = other.transform.position - transform.position;
		int id = other.GetInstanceID ();
		if (aliens.ContainsKey (id)) {
			aliens [id] = force;
		} else {
			aliens.Add(id, force);
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
			if (pColorId != colorId) {
				other.attachedRigidbody.AddForce( aliens[other.GetInstanceID()] * strength);
			}
		}

	}
}

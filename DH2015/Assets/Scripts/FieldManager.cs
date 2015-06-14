using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldManager : MonoBehaviour {
	public int colorId;

	private Dictionary<int, Color> colors;
	private SpriteRenderer renderer;
	
	// Use this for initialization
	void Start () {
		colors = GameManager.colors;
		renderer = GetComponent<SpriteRenderer> ();
		Color color = colors [colorId];
		color.a = 0.5f;
		renderer.color = color;
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
				Player.Kill();
			}
		}
	}
}

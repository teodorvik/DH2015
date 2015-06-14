using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class metaballsScript : MonoBehaviour {
	
	Renderer r;

	// Use this for initialization
	void Start () {
		r = GetComponent<Renderer>();
		r.material.shader = Shader.Find("Cg Metaballs");
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 pos1 = GameObject.Find ("Player 1").transform.position;
		Vector2 pos2 = GameObject.Find ("Player 2").transform.position;
		
		pos1 = Camera.main.WorldToScreenPoint(pos1);
		pos1 [0] /= Screen.width;
		pos1 [1] /= Screen.height;
		
		pos2 = Camera.main.WorldToScreenPoint(pos2);
		pos2 [0] /= Screen.width;
		pos2 [1] /= Screen.height;
		
		Vector4 positions = new Vector4 (pos1 [0], pos1 [1], pos2 [0], pos2 [1]);
		
		GameObject g1 = GameObject.Find("Player 1");
		PlayerBehaviour script1 = (PlayerBehaviour) g1.GetComponent(typeof(PlayerBehaviour));
		int i1 = script1.currentColorId;
		
		GameObject g2 = GameObject.Find("Player 2");
		PlayerBehaviour script2 = (PlayerBehaviour) g2.GetComponent(typeof(PlayerBehaviour));
		int i2 = script2.currentColorId;

		Dictionary<int, Color> colors = GameManager.colors;
		Color c1 = colors[i1];
		Color c2 = colors[i2];
		
		// Send variables to shader
		r.material.SetVector ("_positions", positions);
		r.material.SetVector ("_color1", c1);
		r.material.SetVector ("_color2", c2);
	}
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class metaballsScript : MonoBehaviour {
	private Renderer r;
	private int grey = 0;

	// Use this for initialization
	void Start () {
		r = GetComponent<Renderer>();
		r.material.shader = Shader.Find("Cg Metaballs");
	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		Vector4 xPositions = new Vector4(0f,0f,0f,0f); 
		Vector4 yPositions = new Vector4(0f,0f,0f,0f);
		int i = 0;
		Dictionary<int, Color> colors = GameManager.colors;

		foreach (GameObject player in players) {
			Vector2 pos = player.transform.position;
			pos = Camera.main.WorldToScreenPoint(pos);
			pos [0] /= Screen.width;
			pos [1] /= Screen.height;

			xPositions[i] = pos[0];
			yPositions[i] = pos[1];

			i++;

			PlayerBehaviour script = (PlayerBehaviour) player.GetComponent(typeof(PlayerBehaviour));
			int colorId = script.currentColorId;
			Color color = colors[colorId];

			r.material.SetVector("_color"+i.ToString(), color);
		}

		// Send variables to shader
		r.material.SetVector ("_xPositions", xPositions);
		r.material.SetVector ("_yPositions", yPositions);
		r.material.SetFloat ("_cameraSize", Camera.main.orthographicSize);

		if (Input.GetKeyDown ("g")) {
			if(grey == 1){
				grey = 0;
			} else {
				grey = 1;
			}
			r.material.SetInt("_greyscale", grey);
		}
	}
}
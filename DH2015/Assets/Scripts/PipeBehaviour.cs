using UnityEngine;
using System.Collections;

public class PipeBehaviour : MonoBehaviour {
	public GameObject button;
	private ButtonBehaviour bBehaviour;
	private SpriteRenderer sRend;
	private Color newColor;
	private Color oldColor;
	private bool current = false;
	// Use this for initialization
	void Start () {
		bBehaviour = button.GetComponent<ButtonBehaviour> ();
		sRend = GetComponent<SpriteRenderer> ();
		oldColor = sRend.color;

//		newColor = GameManager.colors [bBehaviour.newColorId];
		newColor.a = 0.25f;
	}
	
	// Update is called once per frame
	void Update () {
		if (bBehaviour.state != current) {
			current = bBehaviour.state;

			if (current) {
				sRend.color = newColor;
			} else {
				sRend.color = oldColor;
			}
		}
	}
}

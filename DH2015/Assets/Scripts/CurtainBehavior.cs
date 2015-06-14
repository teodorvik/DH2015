using UnityEngine;
using System.Collections;

public class CurtainBehavior : MonoBehaviour {
	
	private CanvasGroup canvasGroup;
	private bool xPressed = false;
	public float speed;
	private bool bFadeIn = true;

	void Start () {
		canvasGroup = GetComponent<CanvasGroup> ();
	}

	public void FadeOutToLevel(int level) {
		nextLevel = level;
		bFadeIn = false;

	}

	private int nextLevel;

	void Update() {
		if (bFadeIn) {
			if (canvasGroup.alpha > 0f) {
				canvasGroup.alpha -= 0.01f * speed;
			}
		} else {
			if (canvasGroup.alpha < 1f) {
				canvasGroup.alpha += 0.01f * speed;
			} else {
				Application.LoadLevel(nextLevel);
			}
		}
	}
}

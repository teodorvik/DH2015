using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	private static int inGoal = 0;

	public static Dictionary<int, Color> colors = new Dictionary<int, Color>() {
		{1, new Color(235f/255f,71f/255f,71f/255f)}, // Red
		{2, new Color(126f/255f,235f/255f,71f/255f)}, // Green
		{4, new Color(71f/255f,180f/255f,235f/255f)}, // Blue
		{8, new Color(235f/255f,207f/255f,71f/255f)}, // Yellow
		
		{3, new Color(184f/255f,75f/255f,20f/255f)}, // Red + Green = Brown
		{5, new Color(235f/255f,71f/255f,235f/255f)}, // Red + Blue = Purple
		{9, new Color(235f/255f,139f/255f,71f/255f)}, // Red + Yellow = Orange
		
		{6, new Color(71f/255f,235f/255f,153f/255f)},  // Green + Blue = Turqoise  
		{10, new Color(194f/255f,235f/255f,71f/255f)}, // Green + Yellow = Lime green
		
		{12, new Color(126f/255f,235f/255f,71f/255f)}, // Blue + Yellow = Green
		
		{7, new Color(184f/255f,75f/255f,20f/255f)},  // Red + Green + Blue = Brown
		{11, new Color(184f/255f,75f/255f,20f/255f)}, // Red + Green + Yellow = Brown
		
		{14, new Color(126f/255f,235f/255f,71f/255f)}, // Green + Blue + Yellow = Green
		
		{15, new Color(184f/255f,75f/255f,20f/255f)} // Red + Green + Blue + Yellow = Brown
	};
		
	void Awake()  {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(gameObject);
		}

		DontDestroyOnLoad (gameObject);
	}

	private static int score;

	public static void EnterGoal() {
		score++;
		print (score);
	}

	public static void ExitGoal() {
		score--;
		print (score);
	}

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Colors : MonoBehaviour {

	public enum names {
		White 	= 0,

		Red		= 1,
		Yellow	= 2,
		Green	= 4,
		Blue	= 8,

		Orange	= 3,
		Purple	= 9,
		Lime	= 6,
		Turq	= 12
	};

	private static Dictionary<names, Color> values = new Dictionary<names, Color>() {
		{names.White,	new Color(255f/255f, 255f/255f, 255f/255f)},

		{names.Red,		new Color(232f/255f, 125f/255f, 125f/255f)},
		{names.Yellow,	new Color(232f/255f, 214f/255f, 125f/255f)},
		{names.Green,	new Color(161f/255f, 232f/255f, 125f/255f)},
		{names.Blue,	new Color(125f/255f, 196f/255f, 232f/255f)},

		{names.Orange,	new Color(232f/255f, 170f/255f, 125f/255f)},
		{names.Purple,	new Color(187f/255f, 125f/255f, 232f/255f)},
		{names.Lime,	new Color(205f/255f, 232f/255f, 125f/255f)},
		{names.Turq,	new Color(125f/255f, 232f/255f, 179f/255f)}
	};

	public static Color Get(List<names> colors) {
		int sum = 0;

		foreach (var color in colors) {
			sum += (int)color;
		}

		switch (sum) {

		case 5:
			sum = (int)names.Yellow;
			break;

		case 10:
			sum = (int)names.Green;
			break;

		}


		if (values.ContainsKey((names)sum)) {
			return values[(names)sum];
		} else {
			return values[names.White];
		}
	}

	public static Color Get(names color) {
		return values[color];
	}

	public static names Mix(List<names> colors) {
		int sum = 0;
		
		foreach (var color in colors) {
			sum += (int)color;
		}
		
		switch (sum) {
			
		case 5:
			sum = (int)names.Yellow;
			break;
			
		case 10:
			sum = (int)names.Green;
			break;
			
		}
		
		
		if (values.ContainsKey((names)sum)) {
			return (names)sum;
		} else {
			return names.White;
		}
	}
}

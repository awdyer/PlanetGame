using UnityEngine;
using System.Collections;

public class CrossHair : MonoBehaviour {
	public Texture crosshair;
	public float size = 75.0f;

	void OnGUI(){
		GUI.Label(new Rect(Screen.width/2-(size/2),Screen.height/2-(size/2), size, size), crosshair);
	}
}

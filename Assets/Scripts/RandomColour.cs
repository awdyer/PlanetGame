using UnityEngine;
using System.Collections;

public class RandomColour : MonoBehaviour {

	public Color colour = Color.black;

	void Awake() 
    {
		if (colour == Color.black) 
        {
			colour = new Color (Random.value, Random.value, Random.value);
		}
		GetComponent<Renderer> ().material.color = colour;
	}
}

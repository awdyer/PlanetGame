using UnityEngine;
using System.Collections;

public class SetColour : MonoBehaviour {

    public Color colour = Color.black;
    public bool random = false;

    void Awake()
    {
        if (random)
        {
            colour = new Color(Random.value, Random.value, Random.value);
        }
        GetComponent<Renderer>().material.color = colour;
    }
}

using UnityEngine;
using System.Collections;

public class Scaler : MonoBehaviour {

    private Vector3 scale;
    public float scaleAmount = 1.0001f;
    // Use this for initialization
    void Start()
    {
        scale = new Vector3(scaleAmount, scaleAmount, scaleAmount);
    }

    void FixedUpdate()
    {
        transform.localScale = new Vector3(transform.localScale.x * scaleAmount, transform.localScale.y * scaleAmount, transform.localScale.z * scaleAmount);
    }
}

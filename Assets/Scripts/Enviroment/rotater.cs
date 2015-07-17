using UnityEngine;
using System.Collections;

public class Rotater : MonoBehaviour
{

    public float rotateSpeed = 1.0f;

    private void Update()
    {
        transform.Rotate(new Vector3(0, rotateSpeed, 0) * Time.deltaTime);
    }
}

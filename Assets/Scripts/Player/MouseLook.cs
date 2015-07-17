using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour {

    public float mouseSensitivityX = 250;
    public float mouseSensitivityY = 250;

    private Transform cameraTransform;
    private float verticalLookRotation = 0;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime);
        verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation,-60,60);
        cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;
    }
}

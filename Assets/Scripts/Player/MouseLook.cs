using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    float sensitivityX = 8;

    [SerializeField]
    float sensitivityY = 0.5f;

    [SerializeField]
    float mult = 0.1f;

    [SerializeField]
    Transform playerCamera;

    [SerializeField]
    float xClamp = 85;

    float xRotation = 0;
    float yRotation = 0;

    float mouseX, mouseY;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ReceiveInput(Vector2 mouseInput)
    {
        mouseX = mouseInput.x;
        mouseY = mouseInput.y;
    }

    private void FixedUpdate()
    {
        RotateView();
    }

    void RotateView()
    {
        yRotation += mouseX * sensitivityX * mult;
        xRotation -= mouseY * sensitivityY * mult;
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}

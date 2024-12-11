using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 2f;
    private float cameraVerticalRotation = 0f;
    //public PauseGame pauseScript;
    //private bool lockedCursor = true;
    void Start()
    {
        // Lock and hide the cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;

    }

    void Update()
    {
        if (!PauseGame.isPaused && !PauseGame.isGameOver)
        {
            // Collect mouse input
            float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            // Rotate the camera around its local X axis
            cameraVerticalRotation -= inputY;
            cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
            transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

            // Rotate the player object and the camera around its Y axis
            player.Rotate(Vector3.up * inputX);
        }
        if (PauseGame.isGameOver)
        {
            // Rotate the camera around its local X axis
            cameraVerticalRotation = 0;
            transform.localEulerAngles = Vector3.right * cameraVerticalRotation;
            mouseSensitivity = 0;
        }
        else
        {
            mouseSensitivity = 1;
        }

    }

    void FixedUpdate()
    {

    }
}

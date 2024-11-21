using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float horizontalInput;
    [SerializeField] private float verticalInput;
    [SerializeField] private float movementSpeed = 1f;
    // public float turnSpeed = 10f;
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        // Read values from keyboard
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Move the object
        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * movementSpeed);
        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * movementSpeed);
        // transform.Rotate(Vector3.up * horizontalInput * turnSpeed * Time.deltaTime);
    }
}

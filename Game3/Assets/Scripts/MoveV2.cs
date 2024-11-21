using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveV2 : MonoBehaviour
{
    [SerializeField] private float horizontalInput;
    [SerializeField] private float verticalInput;
    [SerializeField] private float movementSpeed = 1f;

    [SerializeField] private float slopeForce;
    [SerializeField] private float slopeForceRayLength;

    private CharacterController charController;
    
    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Read values from keyboard
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Move the ojbect
        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * movementSpeed);
        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * movementSpeed);
        // transform.Rotate(Vector3.up * horizontalInput * turnSpeed * Time.deltaTime);

        // Checks if on slope
        if ((verticalInput != 0 || horizontalInput != 0) && OnSlope())
        {
            charController.Move(Vector3.down * charController.height / 2 * slopeForce * Time.deltaTime);
        }
    }

    private bool OnSlope()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, charController.height / 2 * slopeForceRayLength))
        {
            if (hit.normal != Vector3.up)
            {  
                return true;
            } 
        }
        return false;
    }
}

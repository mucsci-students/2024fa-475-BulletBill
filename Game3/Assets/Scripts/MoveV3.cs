using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveV3 : MonoBehaviour
{
    [SerializeField] private float horizontalInput;
    [SerializeField] private float verticalInput;
    [SerializeField] private float movementSpeed = 1f;
    private Vector3 destination; // Where to move, in world space.
    private Vector3 currentPos; // Could be transform.position
    private Rigidbody rb;
    // public float turnSpeed = 10f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move();
    }

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
    }
}

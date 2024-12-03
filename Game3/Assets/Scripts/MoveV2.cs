using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveV2 : MonoBehaviour
{
    
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float moveMult = 1f;
    [SerializeField] private float rbDrag = 6f;
    private float horizontalMovement;
    private float verticalMovement;
    private Vector3 moveDirection;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    void Update()
    {
        MyInput();
        ControlDrag();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void ControlDrag()
    {
        rb.drag = rbDrag;
    }

    void MovePlayer()
    {
        rb.AddForce(moveDirection.normalized * moveSpeed * moveMult, ForceMode.Acceleration);
    }

    void MyInput()
    {
       horizontalMovement = Input.GetAxisRaw("Horizontal");
       verticalMovement = Input.GetAxisRaw("Vertical");
       moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
    }
}

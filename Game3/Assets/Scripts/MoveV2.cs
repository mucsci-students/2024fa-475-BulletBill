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
    public PhysicMaterial normalMat, otherMat;
    private bool onStairs = false;
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
        if (!onStairs)
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

    void StairsInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.forward * Time.deltaTime * verticalMovement * moveSpeed);
        transform.Translate(Vector3.right * Time.deltaTime * horizontalMovement * moveSpeed);
    }

    void OnTriggerStay (Collider other)
    {
        if (other.tag == "Stairs")
        {
            Debug.Log("In stairs");
            onStairs = true;
            gameObject.GetComponent<CapsuleCollider>().material = normalMat;
            StairsInput();
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.tag == "Stairs")
        {
            Debug.Log("Exited Stairs");
            onStairs = false;
            gameObject.GetComponent<CapsuleCollider>().material = otherMat;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveV2 : MonoBehaviour
{
    
    [SerializeField] public float moveSpeed = 1f;
    [SerializeField] private float moveMult = 1f;
    [SerializeField] private float rbDrag = 6f;
    [SerializeField] public AudioClip stepSound;
    private float horizontalMovement;
    private float verticalMovement;
    private Vector3 moveDirection;
    public PhysicMaterial normalMat, otherMat;
    private bool onStairs = false;
    private float timer = 0f;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    void Update()
    {
        if (!PauseGame.isPaused && !PauseGame.isGameOver) 
        {
            MyInput();
            ControlDrag();
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                if (timer == 0f)
                {
                    SoundFXManager.instance.PlaySoundFXClip(stepSound, transform, 0.05f);
                }

                timer += Time.deltaTime;
                if (timer >= .85f)
                {
                    SoundFXManager.instance.PlaySoundFXClip(stepSound, transform, 0.05f);
                    timer = 0.1f;
                }
            }
            else
            {
                timer = 0f;
            }
        }
    }

    void FixedUpdate()
    {
        if (!PauseGame.isPaused && !PauseGame.isGameOver)
        {
            if (!onStairs)
            MovePlayer();
        }
        
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
        // if (horizontalMovement != 0 || verticalMovement != 0)
        // {
        //     SoundFXManager.instance.PlaySoundFXClip(stepSound, transform, 0.3f);
        // }
    }

    void OnTriggerStay (Collider other)
    {
        if (other.tag == "Stairs")
        {
            onStairs = true;
            gameObject.GetComponent<CapsuleCollider>().material = normalMat;
            StairsInput();
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.tag == "Stairs")
        {
            onStairs = false;
            gameObject.GetComponent<CapsuleCollider>().material = otherMat;
        }
    }

    IEnumerator walk()
    {
        yield return new WaitForSeconds (2f);
        SoundFXManager.instance.PlaySoundFXClip(stepSound, transform, 0.3f);
    }
}

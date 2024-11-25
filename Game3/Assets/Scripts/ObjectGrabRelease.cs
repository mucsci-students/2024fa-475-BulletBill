using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabRelease : MonoBehaviour
{
    public GameObject player;
    public Camera playerCamera;
    public GameObject[] objectArray = new GameObject[1];
    private GameObject objectInHand;
    private GameObject closestObject;
    private float maxGrabDistance = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        objectInHand = null;
        closestObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        updateClosestItem();
        checkGrabInput();
    }

    void updateClosestItem()
    {
        foreach (GameObject obj in objectArray)
        {
            if (closestObject == null)
            {
                closestObject = obj;
            }
            else
            {
                if (Vector3.Distance(obj.transform.position, transform.position) < Vector3.Distance(closestObject.transform.position, transform.position))
                {
                    closestObject = obj;
                }
            }
        }
    }

    void checkGrabInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (objectInHand == null)
            {
                if (Vector3.Distance(closestObject.transform.position, transform.position) <= maxGrabDistance)
                {
                    updateHeldObject();
                }
            }
            else
            {
                throwObject();
                updateHeldObject();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if (objectInHand != null)
            {
                throwObject();
            }
        }
    }

    void throwObject()
    {
        objectInHand.transform.parent = null;
        objectInHand.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        objectInHand.GetComponent<Rigidbody>().velocity = player.transform.forward * 5;
        objectInHand = null;
    }

    void updateHeldObject()
    {
        objectInHand = closestObject;
        objectInHand.transform.parent = player.transform;
        objectInHand.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        // change the position relative to the parent
        objectInHand.transform.localPosition = new Vector3(1.25f, 1, 1.25f);
        // change the rotation relative to the parent so that it is pointing up
        objectInHand.transform.localRotation = Quaternion.Euler(90, 70, 0);
    }
}

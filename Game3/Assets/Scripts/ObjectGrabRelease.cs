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
    private Vector3[] keyPositions = new Vector3[3];
    private Vector3[] otherPositions = new Vector3[9];
    // Start is called before the first frame update
    void Start()
    {
        objectInHand = null;
        closestObject = null;
        keyPositions[0] = new Vector3(1.279f, 1.076f, 0.013f);
        keyPositions[1] = new Vector3(-.844f, 0.2172f, 1.317f);
        keyPositions[2] = new Vector3(-2.65f, 0.215f, 0.382f);
        otherPositions[0] = new Vector3(-2.409f, 1, -3.489f);
        otherPositions[1] = new Vector3(-1.393f, 0.972f, 2.812f);
        otherPositions[2] = new Vector3(2.585f, 0.887f, 2.096f);
        otherPositions[3] = new Vector3(-3.537f, 0.159f, 3.322f);
        otherPositions[4] = new Vector3(-3.45f, 0.15f, -3.51f);
        otherPositions[5] = new Vector3(2.74f, 0.219f, -2.789f);
        otherPositions[6] = new Vector3(-3.453f, -0.711f, -8.59f);
        otherPositions[7] = new Vector3(2.308f, -0.695f, -7.993f);
        otherPositions[8] = new Vector3(3.74f, 0.889f, -1.149f);
        SetUpObjects();
    }

    // Update is called once per frame
    void Update()
    {
        updateClosestItem();
        checkGrabInput();
    }

    int randomIndexForKeys()
    {
        int index = Random.Range(0, keyPositions.Length);
        if (keyPositions[index].x == 0)
        {
            return randomIndexForKeys();
        }
        return index;
    }

    int randomIndexForOthers()
    {
        int index = Random.Range(0, otherPositions.Length);
        if (otherPositions[index].x == 0)
        {
            return randomIndexForOthers();
        }
        return index;
    }

    void SetUpObjects()
    {
        for (int i = 0; i < 3; i++)
        {
            // get a random position for the keys by randomly choosing a position from the key positions array
            int randomIndex = randomIndexForKeys();
            objectArray[i].transform.position = keyPositions[randomIndex];
            // remove the position from the array so that it can't be chosen again
            keyPositions[randomIndex] = new Vector3(0, 0, 0);
        }
        for (int i = 3; i < objectArray.Length; i++)
        {
            int randomIndex = randomIndexForOthers();
            objectArray[i].transform.position = otherPositions[randomIndex];
            // remove the position from the array so that it can't be chosen again
            otherPositions[randomIndex] = new Vector3(0, 0, 0);
        }
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

    void OnGUI()
    {
        if (objectInHand != null)
        {
            GUI.Box(new Rect(10, 10, 300, 50), "Press Q to throw object");
            GUI.Box(new Rect(10, 70, 300, 50), "Currently holding: " + objectInHand.name);
        }
        else
        {
            GUI.Box(new Rect(10, 10, 300, 50), "Press E to pickup an object");
        }
    }
}

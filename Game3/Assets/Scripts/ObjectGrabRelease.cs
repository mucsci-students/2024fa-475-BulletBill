using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabRelease : MonoBehaviour
{
    public GameObject player;
    public Camera playerCamera;
    public GameObject[] objectArray = new GameObject[13];
    public Animator basementDoor;
    public Animator jailDoor;
    public Animator safeDoor;
    public bool crossBowLoaded = false;
    public bool isBasementDoorOpen = false;
    public GameObject objectInHand = null;
    public GameObject escapeDoor;
    private GameObject closestObject;
    public GameObject arrow;
    public Camera[] cameraArray = new Camera[12];
    private Camera closestCamera;
    private float maxGrabDistance = 1.0f;
    private Vector3[] keyPositions = new Vector3[4];
    private Vector3[] otherPositions = new Vector3[7];
    private Vector3[] hiddenPositions = new Vector3[2];

    private GameObject[] JailObjects = new GameObject[2];
    private GameObject SafeObject;

    //for camera hiding
    public GameObject enemy;
    public SeesPlayer seeScript;
    public EnemySpawn enemyScript;

    private GUIStyle buttonStyle;
    // Start is called before the first frame update
    void Start()
    {
        objectInHand = null;
        closestObject = null;
        keyPositions[0] = new Vector3(7.508f, 0.986f, 0.8f);
        keyPositions[1] = new Vector3(-.844f, 0.2172f, 1.317f);
        keyPositions[2] = new Vector3(-2.65f, 0.215f, 0.382f);
        otherPositions[0] = new Vector3(4.719f, 1.065f, -1.963f);
        otherPositions[1] = new Vector3(5.351f, 0.876f, 1.93f);
        otherPositions[2] = new Vector3(11.005f, 1, -2.948f);
        otherPositions[3] = new Vector3(-3.537f, 0.159f, 3.322f);
        otherPositions[4] = new Vector3(-3.45f, 0.15f, -3.51f);
        otherPositions[5] = new Vector3(2.74f, 0.219f, -2.789f);
        otherPositions[6] = new Vector3(-3.453f, -0.711f, -8.59f);
        hiddenPositions[0] = new Vector3(2.308f, -0.695f, -7.993f);
        hiddenPositions[1] = new Vector3(8.788f, 0.89f, 3.016f);
        SetUpObjects();

        // Initialize the style
        buttonStyle = new GUIStyle();

        // Set the font style to bold
        buttonStyle.fontStyle = FontStyle.Bold;

        // Set the text color to white
        buttonStyle.normal.textColor = Color.white;

        // Create a black texture and assign it as the button's background
        Texture2D blackTexture = new Texture2D(1, 1);
        blackTexture.SetPixel(0, 0, Color.black);
        buttonStyle.normal.background = blackTexture;

        // Adjust font size if needed
        buttonStyle.fontSize = 75;

        // Adjust padding or other properties as needed
        buttonStyle.padding = new RectOffset(10, 10, 10, 10);

        StartCoroutine(findEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        updateClosestItem();
        updateClosestCamera();
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

    int randomIndexForHidden()
    {
        int index = Random.Range(0, hiddenPositions.Length);
        if (hiddenPositions[index].x == 0)
        {
            return randomIndexForHidden();
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
        JailObjects[0] = objectArray[3];
        for (int i = 4; i < objectArray.Length - 2; i++)
        {
            int randomIndex = randomIndexForOthers();
            objectArray[i].transform.position = otherPositions[randomIndex];
            // remove the position from the array so that it can't be chosen again
            otherPositions[randomIndex] = new Vector3(0, 0, 0);
        }
        for (int i = 11; i < objectArray.Length; i++)
        {
            int randomIndex = randomIndexForHidden();
            objectArray[i].transform.position = hiddenPositions[randomIndex];
            // remove the position from the array so that it can't be chosen again
            hiddenPositions[randomIndex] = new Vector3(0, 0, 0);
            if (randomIndex == 0)
            {
                JailObjects[1] = objectArray[i];
            }
            else
            {
                SafeObject = objectArray[i];
            }
        }
    }

    void updateClosestItem()
    {
        foreach (GameObject obj in objectArray)
        {
            if (obj.tag == "Interactable")
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
    }

    void updateClosestCamera()
    {
        foreach (Camera cam in cameraArray)
        {
            if (closestCamera == null)
            {
                closestCamera = cam;
            }
            else
            {
                if (Vector3.Distance(cam.transform.position, transform.position) < Vector3.Distance(closestCamera.transform.position, transform.position))
                {
                    closestCamera = cam;
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
                if (objectInHand.name == "JailKey" && Vector3.Distance(objectInHand.transform.position, jailDoor.transform.position) <= maxGrabDistance)
                {
                    jailDoor.SetTrigger("openDoor");
                    JailObjects[0].tag = "Interactable";
                    print("JailObjects[0]: " + JailObjects[0].name + ", Tag: " + JailObjects[0].tag);
                    JailObjects[1].tag = "Interactable";
                    print("JailObjects[1]: " + JailObjects[1].name + ", Tag: " + JailObjects[1].tag);
                }
                else if (objectInHand.name == "BasementKey" && Vector3.Distance(objectInHand.transform.position, basementDoor.transform.position) <= maxGrabDistance)
                {
                    basementDoor.SetTrigger("openDoor");
                    isBasementDoorOpen = true;
                }
                else if (objectInHand.name == "SafeKey" && Vector3.Distance(objectInHand.transform.position, safeDoor.transform.position) <= maxGrabDistance)
                {
                    safeDoor.SetTrigger("openDoor");
                    SafeObject.tag = "Interactable";
                    print("SafeObject: " + SafeObject.name + ", Tag: " + SafeObject.tag);
                }
                else if (objectInHand.name == "Crossbow" && Vector3.Distance(arrow.transform.position, transform.position) <= maxGrabDistance && !crossBowLoaded)
                {
                    print("Loading arrow");
                    crossBowLoaded = true;
                    LoadArrow();
                }
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
        else if (Input.GetKeyDown(KeyCode.R))
        {
            if (closestCamera != null && Vector3.Distance(closestCamera.transform.position, transform.position) <= maxGrabDistance)
            {
                playerCamera.enabled = false;
                closestCamera.enabled = true;
                closestCamera.GetComponent<AudioListener>().enabled = true;
                // set the player as inactive
                player.SetActive(false);

                //
                seeScript.isFollowingPlayer = false;
                enemyScript.clone.GetComponent<EnemyControl>().target = enemyScript.currentLocation;
                Debug.Log("Player is hiding");
            }
        }
    }

    void throwObject()
    {
        objectInHand.GetComponent<BoxCollider>().enabled = true;
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
        if (objectInHand.name != "Crossbow")
        {
            objectInHand.transform.localRotation = Quaternion.Euler(90, 70, 0);
        }
        else
        {
            objectInHand.transform.localRotation = Quaternion.Euler(0, 170, 0);
        }
        objectInHand.GetComponent<BoxCollider>().enabled = false;
    }

    void LoadArrow()
    {
        arrow.transform.parent = objectInHand.transform;
        arrow.GetComponent<BoxCollider>().enabled = false;
        arrow.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        arrow.transform.localPosition = new Vector3(0, 0, -.5f);
        arrow.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    IEnumerator findEnemy()
    {
        yield return new WaitForSeconds (2f);
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        seeScript = enemy.transform.GetChild(4).GetComponent<SeesPlayer>();
    }

    void OnGUI()
    {
        if (objectInHand != null)
        {
            GUI.Box(new Rect(10, 10, 1250, 100), "Press Q to throw object", buttonStyle);
            GUI.Box(new Rect(10, 110, 1250, 100), "Currently holding: " + objectInHand.name, buttonStyle);
        }
        else
        {
            GUI.Box(new Rect(10, 10, 1250, 100), "Press E to pickup an object", buttonStyle);
        }
        
        if (Vector3.Distance(player.transform.position, escapeDoor.transform.position) <= 1.0f)
        {
            GUI.Box(new Rect(10, 210, 1250, 100), "Items need to escape (In Order):", buttonStyle);
            GUI.Box(new Rect(10, 310, 1250, 100), "1. Bolt Cutters", buttonStyle);
            GUI.Box(new Rect(10, 410, 1250, 100), "2. Screwdriver", buttonStyle);
            GUI.Box(new Rect(10, 510, 1250, 100), "3. Crowbar", buttonStyle);
            GUI.Box(new Rect(10, 610, 1250, 100), "4. Electricity Turned On (Lever)", buttonStyle);
            GUI.Box(new Rect(10, 710, 1250, 100), "5. Escape Key", buttonStyle);
        }

        if (Vector3.Distance(player.transform.position, closestCamera.transform.position) <= maxGrabDistance && closestCamera != null)
        {
            GUI.Box(new Rect(10, 810, 1250, 100), "Press R to hide", buttonStyle);
        }
    }
}

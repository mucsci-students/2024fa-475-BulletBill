using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabRelease : MonoBehaviour
{
    [SerializeField] public AudioClip keyUnlockSound;
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
    public GameObject chainsOn;
    public GameObject chainsOff;
    public GameObject screwsOn;
    public GameObject screwsOff;
    public GameObject planksOn;
    public GameObject planksOff;
    public GameObject yellowBox;
    private GameObject closestObject;
    public GameObject arrow;
    public Camera[] cameraArray = new Camera[12];
    private Camera closestCamera;
    private float maxGrabDistance = 0.8f;
    private Vector3[] keyPositions = new Vector3[8];
    private Vector3[] otherPositions = new Vector3[20];
    private Vector3[] hiddenPositions = new Vector3[2];

    private GameObject JailObject;
    private GameObject SafeObject;
    //private bool basementDoorOpen = false;
    private bool jailDoorOpen = false;
    private bool safeOpen = false;
    private bool chainsOffBool = false;
    private bool screwsOffBool = false;
    private bool planksOffBool = false;
    private bool ElectricityOn = false;

    //for camera hiding
    public GameObject enemy;
    public SeesPlayer seeScript;
    public EnemySpawn enemyScript;

    private GUIStyle buttonStyle;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("ElectricityOn", 0);
        objectInHand = null;
        closestObject = null;
        keyPositions[0] = new Vector3(7.508f, 0.986f, 0.8f); // master bedroom table
        keyPositions[1] = new Vector3(-.844f, 0.2172f, 1.317f); // living room desk
        keyPositions[2] = new Vector3(-2.65f, 0.215f, 0.382f); // couch table
        keyPositions[3] = new Vector3(9.6585f, 0.986f, 2.6516f); //master bed
        keyPositions[4] = new Vector3(4.781f, 1.005f, -3.776f); // bed1
        keyPositions[5] = new Vector3(7.925f, 1.005f, -2.709f); // bed2
        keyPositions[6] = new Vector3(3.01f, 0.2164f, 1.5824f); // small desk
        keyPositions[7] = new Vector3(3.611f, 0.2146f, 0.6027f); // computer desk in powerbox room
        otherPositions[0] = new Vector3(4.1347f, 1f, 1.9884f); // upstairs bathroom towels
        otherPositions[1] = new Vector3(5.351f, 0.876f, 1.93f);
        otherPositions[2] = new Vector3(11.005f, 1, -2.948f);
        otherPositions[3] = new Vector3(-3.537f, 0.159f, 3.322f);
        otherPositions[4] = new Vector3(-3.45f, 0.15f, -3.51f);
        otherPositions[5] = new Vector3(2.74f, 0.219f, -2.789f);
        otherPositions[6] = new Vector3(-3.453f, -0.711f, -8.59f);
        otherPositions[7] = new Vector3(4.67f, 0.99f, -0.56f);
        otherPositions[8] = new Vector3(3.37f, 0.12f, 1.03f);
        otherPositions[9] = new Vector3(1.719f, 0.12f, 1.03f); // power box room corner
        otherPositions[10] = new Vector3(-3.453f, -0.587f, -7.78f); // game room
        otherPositions[11] = new Vector3(-0.9f, -0.587f, -7.78f); // game room
        otherPositions[12] = new Vector3(-1.49f, -0.525f, -6.165f); // hockey table
        otherPositions[13] = new Vector3(1.783f, -0.652f, -6.165f); // gym room
        otherPositions[14] = new Vector3(3.159f, -0.652f, -6.165f); // gym room
        otherPositions[15] = new Vector3(2.88f, -0.65f, -4.226f); // gym room
        otherPositions[16] = new Vector3(-2.252f, -0.549f, -10.994f); // between boxes in power room
        otherPositions[17] = new Vector3(-3.591f, 0.221f, 0.19f); // on tv
        otherPositions[18] = new Vector3(2.1054f, 0.1742f, 1.7597f); // on drumset
        otherPositions[19] = new Vector3(3.154f, -0.65f, -9.47f); // bottom jail
        hiddenPositions[0] = new Vector3(2.308f, -0.695f, -7.993f); //jail
        hiddenPositions[1] = new Vector3(8.788f, 0.89f, 3.016f); // safe
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
        if (PlayerPrefs.GetInt("ElectricityOn") == 1)
        {
            ElectricityOn = true;
        }
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
        for (int i = 0; i < 2; i++)
        {
            // get a random position for the keys by randomly choosing a position from the key positions array
            int randomIndex = randomIndexForKeys();
            objectArray[i].transform.position = keyPositions[randomIndex];
            // remove the position from the array so that it can't be chosen again
            keyPositions[randomIndex] = new Vector3(0, 0, 0);
        }
        for (int i = 2; i < objectArray.Length - 2; i++)
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
            if (randomIndex == 0 || randomIndex == 2)
            {
                JailObject = objectArray[i];
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
                    jailDoorOpen = true;
                    SoundFXManager.instance.PlaySoundFXClip(keyUnlockSound, transform, 0.6f);
                    jailDoor.SetTrigger("openDoor");
                    JailObject.tag = "Interactable";
                    print("JailObject: " + JailObject.name + ", Tag: " + JailObject.tag);
                }
                else if (objectInHand.name == "BasementKey" && Vector3.Distance(objectInHand.transform.position, basementDoor.transform.position) <= maxGrabDistance)
                {
                    basementDoor.SetTrigger("openDoor");
                    SoundFXManager.instance.PlaySoundFXClip(keyUnlockSound, transform, 0.6f);
                    isBasementDoorOpen = true;
                }
                else if (objectInHand.name == "SafeKey" && Vector3.Distance(objectInHand.transform.position, safeDoor.transform.position) <= maxGrabDistance)
                {
                    safeOpen = true;
                    SoundFXManager.instance.PlaySoundFXClip(keyUnlockSound, transform, 0.6f);
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
                else if (objectInHand.name == "BoltCutters" && Vector3.Distance(escapeDoor.transform.position, transform.position) <= maxGrabDistance)
                {
                    print("Removing chains");
                    chainsOn.SetActive(false);
                    chainsOff.SetActive(true);
                    chainsOffBool = true;
                }
                else if (objectInHand.name == "Screwdriver" && Vector3.Distance(escapeDoor.transform.position, transform.position) <= maxGrabDistance && chainsOffBool)
                {
                    print("Removing screws");
                    screwsOn.SetActive(false);
                    screwsOff.SetActive(true);
                    screwsOffBool = true;
                }
                else if (objectInHand.name == "Crowbar" && Vector3.Distance(escapeDoor.transform.position, transform.position) <= maxGrabDistance && screwsOffBool)
                {
                    print("Removing planks");
                    planksOn.SetActive(false);
                    planksOff.SetActive(true);
                    planksOffBool = true;
                }
                else if (objectInHand.name == "EscapeKey" && Vector3.Distance(escapeDoor.transform.position, transform.position) <= maxGrabDistance && planksOffBool)
                {
                    print("Opening escape door");
                    escapeDoor.SetActive(false);
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
        if (objectInHand.name == "Taser")
        {
            objectInHand.transform.localRotation = Quaternion.Euler(0, 90, 0);
        }
        else if (objectInHand.name == "Crossbow")
        {
            objectInHand.transform.localRotation = Quaternion.Euler(0, 170, 0);
        }
        else if (objectInHand.name == "BoltCutters")
            objectInHand.transform.localRotation = Quaternion.Euler(90, -90, -70);
        else
        {
            objectInHand.transform.localRotation = Quaternion.Euler(90, 70, 0);
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
        arrow.tag = "NonInteractable";
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
        
        if (Vector3.Distance(player.transform.position, escapeDoor.transform.position) <= maxGrabDistance)
        {
            GUI.Box(new Rect(10, 210, 1250, 100), "Items need to escape (In Order):", buttonStyle);
            GUI.Box(new Rect(10, 310, 1250, 100), "1. Bolt Cutters", buttonStyle);
            GUI.Box(new Rect(10, 410, 1250, 100), "2. Screwdriver", buttonStyle);
            GUI.Box(new Rect(10, 510, 1250, 100), "3. Crowbar", buttonStyle);
            GUI.Box(new Rect(10, 610, 1250, 100), "4. Electricity Turned On (Lever)", buttonStyle);
            GUI.Box(new Rect(10, 710, 1250, 100), "5. Escape Key", buttonStyle);

            if (chainsOffBool)
            {
                if (screwsOffBool)
                {
                    if (planksOffBool)
                    {
                        if (ElectricityOn)
                        {
                            if (objectInHand == null || objectInHand.name != "EscapeKey")
                            {
                                GUI.Box(new Rect(10, 810, 1400, 100), "Escape Key needed to escape", buttonStyle);
                            }
                            else if (objectInHand.name == "EscapeKey")
                            {
                                GUI.Box(new Rect(10, 810, 1400, 100), "Press E to escape", buttonStyle);
                            }
                        }
                        else
                        {
                            GUI.Box(new Rect(10, 810, 1400, 100), "Electricity needed to escape", buttonStyle);
                        }
                    }
                    else
                    {
                        if (objectInHand == null || objectInHand.name != "Crowbar")
                        {
                            GUI.Box(new Rect(10, 810, 1400, 100), "Crowbar needed to remove planks", buttonStyle);
                        }
                        else if (objectInHand.name == "Crowbar")
                        {
                            GUI.Box(new Rect(10, 810, 1400, 100), "Press E to remove planks", buttonStyle);
                        }
                    }
                }
                else
                {
                    if (objectInHand == null || objectInHand.name != "Screwdriver")
                    {
                        GUI.Box(new Rect(10, 810, 1400, 100), "Screwdriver needed to remove screws", buttonStyle);
                    }
                    else if (objectInHand.name == "Screwdriver")
                    {   
                        GUI.Box(new Rect(10, 810, 1400, 100), "Press E to remove screws", buttonStyle);
                    }
                }
            }
            else
            {
                if (objectInHand == null || objectInHand.name != "BoltCutters")
                {
                    GUI.Box(new Rect(10, 810, 1400, 100), "Bolt Cutters needed to remove chains", buttonStyle);
                }
                else if (objectInHand.name == "BoltCutters")
                {   
                    GUI.Box(new Rect(10, 810, 1400, 100), "Press E to remove chains", buttonStyle);
                }
            }
        }

        if (!isBasementDoorOpen)
        {
            if (Vector3.Distance(player.transform.position, basementDoor.transform.position) <= 1.0f)
            {
                if (objectInHand == null || objectInHand.name != "BasementKey")
                {
                    GUI.Box(new Rect(10, 210, 1250, 100), "Basement Key needed to unlock", buttonStyle);
                }
                else if (objectInHand.name == "BasementKey")
                {
                    GUI.Box(new Rect(10, 210, 1350, 100), "Press E to open the basement door", buttonStyle);
                }
            }
        }

        if (!jailDoorOpen)
        {
            if (Vector3.Distance(player.transform.position, jailDoor.transform.position) <= 1.0f)
            {
                if (objectInHand == null || objectInHand.name != "JailKey")
                {
                    GUI.Box(new Rect(10, 310, 1250, 100), "Jail Key needed to unlock", buttonStyle);
                }
                else if (objectInHand.name == "JailKey")
                {
                    GUI.Box(new Rect(10, 310, 1350, 100), "Press E to open the jail door", buttonStyle);
                }
            }
        }

        if (!safeOpen)
        {
            if (Vector3.Distance(player.transform.position, safeDoor.transform.position) <= 1.0f)
            {
                if (objectInHand == null || objectInHand.name != "SafeKey")
                {
                    GUI.Box(new Rect(10, 410, 1250, 100), "Safe Key needed to unlock", buttonStyle);
                }
                else if (objectInHand.name == "SafeKey")
                {
                    GUI.Box(new Rect(10, 410, 1350, 100), "Press E to open the safe", buttonStyle);
                }
            }
        }

        if (!ElectricityOn)
        {
            if (Vector3.Distance(player.transform.position, yellowBox.transform.position) <= 1.0f)
            {
                if (objectInHand == null || objectInHand.name != "Lever")
                {
                    GUI.Box(new Rect(10, 410, 1250, 100), "Lever Needed to turn on", buttonStyle);
                }
                else if (objectInHand.name == "Lever")
                {
                    GUI.Box(new Rect(10, 410, 1350, 100), "Click to turn on the electricity", buttonStyle);
                }
            }
        } 

        if (Vector3.Distance(player.transform.position, closestCamera.transform.position) <= maxGrabDistance && closestCamera != null)
        {
            GUI.Box(new Rect(10, 810, 1250, 100), "Press R to hide", buttonStyle);
        }
    }
}

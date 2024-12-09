using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redToGreenLight : MonoBehaviour
{
    public ObjectGrabRelease script;
    public GameObject hiddenLever;
    public GameObject basementRedLight;
    public GameObject basementGreenLight;
    public GameObject exitRedLight;
    public GameObject exitGreenLight;
    public GameObject box;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (script.objectInHand != null)
        {
            if (script.objectInHand.name == "Lever" && Vector3.Distance(gameObject.transform.position, box.transform.position) < 1.0f && Input.GetButtonDown("Fire1"))
            {
                hiddenLever.SetActive(true);
                basementRedLight.SetActive(false);
                exitRedLight.SetActive(false);
                basementGreenLight.SetActive(true);
                exitGreenLight.SetActive(true);
                script.objectInHand.transform.parent = null;
                script.objectInHand.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                script.objectInHand.transform.position = new Vector3(0, -30, 0);
                script.objectInHand = null;
            }
        }
        
    }
}

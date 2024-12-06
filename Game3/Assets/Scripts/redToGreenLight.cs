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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (script.objectInHand != null)
        {
            if (script.objectInHand.name == "Lever")
            {
                hiddenLever.SetActive(true);
                basementRedLight.SetActive(false);
                exitRedLight.SetActive(false);
                basementGreenLight.SetActive(true);
                exitGreenLight.SetActive(true);
            }
        }
        
    }
}

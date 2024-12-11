using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redToGreenLight : MonoBehaviour
{
    [SerializeField] public AudioClip electricitySound;
    public ObjectGrabRelease script;
    public GameObject hiddenLever;
    public GameObject basementRedLight;
    public GameObject basementGreenLight;
    public GameObject exitRedLight;
    public GameObject exitGreenLight;
    public GameObject box;

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
                script.objectInHand.transform.position = new Vector3(3.25f, 0.377f, -10.734f);
                SoundFXManager.instance.PlaySoundFXClip(electricitySound, transform, 0.4f);
                script.objectInHand = null;
                PlayerPrefs.SetInt("ElectricityOn", 1);
            }
        }
        
    }
}

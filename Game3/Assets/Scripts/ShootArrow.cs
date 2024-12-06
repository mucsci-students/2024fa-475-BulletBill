using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    [SerializeField] public AudioClip arrowSound;
    [SerializeField] private float arrowSpeed = 5f;
    public Camera fpsCam;
    public ObjectGrabRelease script;
    public GameObject arrow;
    void Start()
    {
        script.crossBowLoaded = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (script.objectInHand != null)
        {
            if (script.crossBowLoaded && Input.GetButtonDown("Fire1") && script.objectInHand.name == "Crossbow")
            {
                Shoot();
                SoundFXManager.instance.PlaySoundFXClip(arrowSound, transform, 0.5f);
            }
        }
    }
    void Shoot ()
    {
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        //arrow.transform.rotation = Quaternion.LookRotation(rb.velocity);
        rb.velocity = fpsCam.transform.forward * arrowSpeed;

    }
}

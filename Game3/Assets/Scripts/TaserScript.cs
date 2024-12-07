using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaserScript : MonoBehaviour
{
    //public float range = 100f;
    [SerializeField] public AudioClip taserSound;
    public Camera fpsCam;
    public ObjectGrabRelease script;
    private bool canShoot = true;
    private float timer = 0f;
    void Update()
    {
        if (script.objectInHand != null)
        {
            if (canShoot && Input.GetButtonDown("Fire1") && script.objectInHand.name == "Taser")
            {
                Shoot();
                SoundFXManager.instance.PlaySoundFXClip(taserSound, transform, 0.5f);
                canShoot = false;
            }
        }
        if (!canShoot)
        {
            timer += Time.deltaTime;
            if (timer >= 3)
            {
                canShoot = true;
                timer = 0;
            }
        }
    }

    void Shoot ()
    {
        RaycastHit hit;
        //Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range);
        // if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, Mathf.Infinity, layerMask))
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);
            //hit.collider.gameObject.SetActive(false);

            if (hit.collider.gameObject.tag == "Enemy")
            {

                hit.collider.gameObject.SetActive(false); // Set the object to active

            }
            
        }
    }
}

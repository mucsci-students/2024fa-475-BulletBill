using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaserScript : MonoBehaviour
{
    //public float range = 100f;
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
                canShoot = false;
            }
        }
        if (!canShoot)
        {
            timer += Time.deltaTime;
            if (timer >= 30)
            {
                canShoot = true;
            }
        }
    }

    void Shoot ()
    {
        RaycastHit hit;
        //Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range);
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);
            //hit.collider.gameObject.SetActive(false);

            if (hit.collider.gameObject.name == "Enemy")
            {

                hit.collider.gameObject.SetActive(false); // Set the object to active

            }
            
        }
    }
}

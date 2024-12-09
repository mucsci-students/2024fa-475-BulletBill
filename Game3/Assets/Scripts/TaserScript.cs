using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaserScript : MonoBehaviour
{
    //public float range = 100f;
    [SerializeField] public AudioClip taserSound;
    [SerializeField] public float coolDownTime = 30f;
    public Camera fpsCam;
    public ObjectGrabRelease script;
    private bool canShoot = true;
    private float timer;
    private bool isHit = false;
    private GUIStyle taserStyle;

    void Start()
    {
        timer = coolDownTime;

        // Initialize the style
        taserStyle = new GUIStyle();

        // Set the font style to bold
        taserStyle.fontStyle = FontStyle.Bold;

        // Set the text color to white
        taserStyle.normal.textColor = Color.white;

        // Create a black texture and assign it as the button's background
        Texture2D blackTexture = new Texture2D(1, 1);
        blackTexture.SetPixel(0, 0, Color.black);
        taserStyle.normal.background = blackTexture;

        // Adjust font size if needed
        taserStyle.fontSize = 75;

        // Adjust padding or other properties as needed
        taserStyle.padding = new RectOffset(10, 10, 10, 10);
    }

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
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                canShoot = true;
                timer = coolDownTime;
            }
            if (timer <= 27f)
            {
                isHit = false;
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
                isHit = true;
                hit.collider.gameObject.SetActive(false); // Set the object to active

            }
            
        }
    }
    void OnGUI()
    {
        if (script.objectInHand != null && script.objectInHand.name == "Taser")
        {
            if (timer < 30f)
            {
                GUI.Box(new Rect(2700, 10, 1250, 100), "Taser Cooldown: " + timer.ToString("F2"), taserStyle);
            }
            else
            {
                GUI.Box(new Rect(2700, 10, 1250, 100), "Taser Ready! Click to Shoot!", taserStyle);
            }
        }
        if (isHit)
        {
            GUI.Box(new Rect(1200, 800, 1500, 100), "Enemy hit! Respawning in 15 seconds", taserStyle);
        }
    }
}

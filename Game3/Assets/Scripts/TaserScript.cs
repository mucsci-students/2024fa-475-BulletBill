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
    public EnemySpawn enemyScript;
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
        taserStyle.fontSize = 35;

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
            //hit.collider.gameObject.SetActive(false);

            if (hit.collider.gameObject.tag == "Enemy")
            {
                isHit = true;
                enemyScript.hitTaser = true;
                hit.collider.gameObject.SetActive(false); // Set the object to active
                //Destroy (hit.collider.gameObject);

            }
            
        }
    }
    void OnGUI()
    {
        if (script.objectInHand != null && script.objectInHand.name == "Taser")
        {
            if (timer < 30f)
            {
                GUI.Box(new Rect(1300, 10, 800, 50), "Taser Cooldown: " + timer.ToString("F2"), taserStyle);
            }
            else
            {
                GUI.Box(new Rect(1300, 10, 800, 50), "Taser Ready! Click to Shoot!", taserStyle);
            }
        }
        if (isHit)
        {
            GUI.Box(new Rect(575, 500, 800, 50), "Enemy hit! Respawning in 15 seconds", taserStyle);
        }
    }
}

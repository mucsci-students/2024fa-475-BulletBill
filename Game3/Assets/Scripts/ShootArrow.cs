using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    [SerializeField] public AudioClip arrowSound;
    [SerializeField] private float arrowSpeed = 7.5f;
    public Camera fpsCam;
    public ObjectGrabRelease script;
    public GameObject arrow;
    private GUIStyle crossbowStyle;
    private float timer = 0f;
    private float arrowTimer = 0f;
    public bool isHit = false;
    public bool hasArrowBeenFired = false;
    void Start()
    {
        // Initialize the style
        crossbowStyle = new GUIStyle();

        // Set the font style to bold
        crossbowStyle.fontStyle = FontStyle.Bold;

        // Set the text color to white
        crossbowStyle.normal.textColor = Color.white;

        // Create a black texture and assign it as the button's background
        Texture2D blackTexture = new Texture2D(1, 1);
        blackTexture.SetPixel(0, 0, Color.black);
        crossbowStyle.normal.background = blackTexture;

        // Adjust font size if needed
        crossbowStyle.fontSize = 35;

        // Adjust padding or other properties as needed
        crossbowStyle.padding = new RectOffset(10, 10, 10, 10);
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
        if (isHit)
        {
            timer += Time.deltaTime;
            if (timer >= 3f)
            {
                isHit = false;
                timer = 0f;
            }
        }
        if (hasArrowBeenFired)
        {
            arrowTimer += Time.deltaTime;
            if (arrowTimer >= 3f)
            {
                hasArrowBeenFired = false;
                arrowTimer = 0f;
            }
        }
    }
    void Shoot ()
    {
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.None;
        arrow.GetComponent<BoxCollider>().enabled = true;
        arrow.transform.parent = null;
        //arrow.transform.rotation = Quaternion.LookRotation(rb.velocity);
        rb.velocity = fpsCam.transform.forward * arrowSpeed;
        hasArrowBeenFired = true;
        arrow.tag = "Interactable";
        script.crossBowLoaded = false;
    }

    void OnGUI()
    {
        if (script.objectInHand != null && script.objectInHand.name == "Crossbow")
        {
            if (script.crossBowLoaded)
            {
                GUI.Box(new Rect(1300, 10, 800, 50), "Crossbow is loaded. Click to fire!", crossbowStyle);
            }
            else
            {
                GUI.Box(new Rect(1300, 10, 800, 50), "Requires arrow to be loaded to fire", crossbowStyle);
            }
        }   
        if (isHit)
        {
            GUI.Box(new Rect(575, 500, 800, 50), "Enemy hit! Respawning in 20 seconds", crossbowStyle);
        }
    }
}

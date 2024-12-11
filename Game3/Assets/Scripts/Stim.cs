using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stim : MonoBehaviour
{
    public ObjectGrabRelease script;
    public GameObject canvas;
    public GameObject stim;
    public MoveV2 moveScript;
    private float oldSpeed;
    private bool isBoosted = false;
    private float timer = 0f;
    private GUIStyle stimStyle;
    void Start()
    {
        canvas.SetActive(false);

        oldSpeed = moveScript.moveSpeed;
         // Initialize the style
        stimStyle = new GUIStyle();

        // Set the font style to bold
        stimStyle.fontStyle = FontStyle.Bold;

        // Set the text color to white
        stimStyle.normal.textColor = Color.white;

        // Create a black texture and assign it as the button's background
        Texture2D blackTexture = new Texture2D(1, 1);
        blackTexture.SetPixel(0, 0, Color.black);
        stimStyle.normal.background = blackTexture;

        // Adjust font size if needed
        stimStyle.fontSize = 75;

        // Adjust padding or other properties as needed
        stimStyle.padding = new RectOffset(10, 10, 10, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (script.objectInHand != null)
        {
            if (script.objectInHand.name == "Stim" && Input.GetButtonDown("Fire1"))
            {
                isBoosted = true;
                canvas.SetActive(true);
                moveScript.moveSpeed = oldSpeed * 1.15f;
                script.objectInHand.transform.parent = null;
                script.objectInHand = null;
                stim.transform.position = new Vector3(3f, 0.377f, -10.734f);
                stim.SetActive(false);
            }
        }

        if (isBoosted)
        {
            timer += Time.deltaTime;
            if (timer >= 30)
            {
                canvas.SetActive(false);
                moveScript.moveSpeed = oldSpeed;
                isBoosted = false;
                timer = 0f;
            }
        }
    }

    void OnGUI()
    {
        if (script.objectInHand != null && script.objectInHand.name == "Stim")
        {
            GUI.Box(new Rect(2400, 10, 1400, 100), "Click to inject stim for a speed boost!", stimStyle);
        }
    }
}

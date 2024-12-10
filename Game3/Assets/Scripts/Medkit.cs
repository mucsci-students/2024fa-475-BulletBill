using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    public ObjectGrabRelease script;
    public EnemyAttack attackScript;
    private GUIStyle medkitStyle;
    void Start()
    {
         // Initialize the style
        medkitStyle = new GUIStyle();

        // Set the font style to bold
        medkitStyle.fontStyle = FontStyle.Bold;

        // Set the text color to white
        medkitStyle.normal.textColor = Color.white;

        // Create a black texture and assign it as the button's background
        Texture2D blackTexture = new Texture2D(1, 1);
        blackTexture.SetPixel(0, 0, Color.black);
        medkitStyle.normal.background = blackTexture;

        // Adjust font size if needed
        medkitStyle.fontSize = 75;

        // Adjust padding or other properties as needed
        medkitStyle.padding = new RectOffset(10, 10, 10, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (script.objectInHand != null)
        {
            if (script.objectInHand.name == "Medkit" && Input.GetButtonDown("Fire1"))
            {
                attackScript.health = 3;
                script.objectInHand.transform.parent = null;
                script.objectInHand.SetActive(false);
                script.objectInHand = null;
            }
        }
    }

    void OnGUI()
    {
        if (script.objectInHand != null && script.objectInHand.name == "Medkit")
        {
            GUI.Box(new Rect(2500, 10, 1250, 100), "Click to use medkit to heal", medkitStyle);
        }
    }
}
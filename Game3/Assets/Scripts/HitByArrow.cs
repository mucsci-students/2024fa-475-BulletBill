using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitByArrow : MonoBehaviour
{
    private GUIStyle hitStyle;
    public ShootArrow script;
    void Start()
    {
        // Initialize the style
        hitStyle = new GUIStyle();

        // Set the font style to bold
        hitStyle.fontStyle = FontStyle.Bold;

        // Set the text color to white
        hitStyle.normal.textColor = Color.white;

        // Create a black texture and assign it as the button's background
        Texture2D blackTexture = new Texture2D(1, 1);
        blackTexture.SetPixel(0, 0, Color.black);
        hitStyle.normal.background = blackTexture;

        // Adjust font size if needed
        hitStyle.fontSize = 75;

        // Adjust padding or other properties as needed
        hitStyle.padding = new RectOffset(10, 10, 10, 10);
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.name == "Arrow")
        {
            script.isHit = true;
            Destroy (other);
            //gameObject.SetActive(false);
        }
    }
}

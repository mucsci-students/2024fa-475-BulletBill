using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingToggleOff : MonoBehaviour
{
    public GameObject player;
    public Camera playerCamera;
    public Camera hidingCamera;
    public EnemySpawn script;
    public SeesPlayer seeScript;
    private GUIStyle buttonStyle;
    // Start is called before the first frame update
    void Start()
    {
        // Initialize the style
        buttonStyle = new GUIStyle();

        // Set the font style to bold
        buttonStyle.fontStyle = FontStyle.Bold;

        // Set the text color to white
        buttonStyle.normal.textColor = Color.white;

        // Create a black texture and assign it as the button's background
        Texture2D blackTexture = new Texture2D(1, 1);
        blackTexture.SetPixel(0, 0, Color.black);
        buttonStyle.normal.background = blackTexture;

        // Adjust font size if needed
        buttonStyle.fontSize = 75;

        // Adjust padding or other properties as needed
        buttonStyle.padding = new RectOffset(10, 10, 10, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCamera.enabled == false && hidingCamera.enabled == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                playerCamera.enabled = true;
                hidingCamera.enabled = false;
                player.SetActive(true);
            }
        }
    }

    void OnGUI()
    {
        if(hidingCamera.enabled == true)
        {
            GUI.Box(new Rect(10, 10, 1250, 100), "Press R to stop hiding", buttonStyle);
            script.clone.GetComponent<EnemyControl>().target = null;
            seeScript.isFollowingPlayer = false;
        }
    }
}

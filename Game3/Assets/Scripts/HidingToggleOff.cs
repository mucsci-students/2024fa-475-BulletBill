using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HidingToggleOff : MonoBehaviour
{
    public GameObject player;
    public Camera playerCamera;
    public Camera hidingCamera;
    // public GameObject enemy;
    // public SeesPlayer seeScript;
    private GUIStyle buttonStyle;
    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(findEnemy());
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
        buttonStyle.fontSize = 35;

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
                hidingCamera.GetComponent<AudioListener>().enabled = false;
                hidingCamera.enabled = false;
                player.SetActive(true);
            }
        }
    }

    // IEnumerator findEnemy()
    // {
    //     yield return new WaitForSeconds (2f);
    //     enemy = GameObject.FindGameObjectWithTag("Enemy");
    //     seeScript = enemy.transform.GetChild(4).GetComponent<SeesPlayer>();
    // }

    void OnGUI()
    {
        if(hidingCamera.enabled == true)
        {
            GUI.Box(new Rect(10, 10, 700, 50), "Press R to stop hiding", buttonStyle);
        }
    }
}

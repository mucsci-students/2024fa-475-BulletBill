using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    public ObjectGrabRelease script;
    public GameObject enemy;
    public GameObject medkit;
    //public EnemyAttack attackScript;
    public PlayerHealth healthScript;
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
        medkitStyle.fontSize = 35;

        // Adjust padding or other properties as needed
        medkitStyle.padding = new RectOffset(10, 10, 10, 10);

        //StartCoroutine (findEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        if (script.objectInHand != null)
        {
            if (script.objectInHand.name == "Medkit" && Input.GetButtonDown("Fire1"))
            {
                healthScript.heal();
                script.objectInHand.transform.parent = null;
                script.objectInHand = null;
                medkit.transform.position = new Vector3(1.5f, 0.377f, -10.734f);
                medkit.SetActive(false);
                
            }
        }
    }

    // IEnumerator findEnemy()
    // {
    //     yield return new WaitForSeconds (2f);
    //     enemy = GameObject.FindGameObjectWithTag("Enemy");
    //     attackScript = enemy.transform.GetChild(3).GetComponent<EnemyAttack>();
    // }

    void OnGUI()
    {
        if (script.objectInHand != null && script.objectInHand.name == "Medkit")
        {
            GUI.Box(new Rect(1300, 10, 800, 50), "Click to use medkit to heal", medkitStyle);
        }
    }
}

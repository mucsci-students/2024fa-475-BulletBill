using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeToEscape : MonoBehaviour
{
    public float currentTime = 0f; // Starting time in seconds

    public Text timerText;



    void Update()
    {
        if (!PauseGame.isPaused && !PauseGame.isWin)
        {
            currentTime += Time.deltaTime;
        }    
        string formattedTime = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(currentTime / 60), Mathf.FloorToInt(currentTime % 60));
        timerText.text = formattedTime;
    }
}

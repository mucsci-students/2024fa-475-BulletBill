using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{

    public GameObject PauseMenuUI;
    public GameObject GameOverUI;
    public GameObject CrosshairUI;

    // public GameObject[] player;
    public void GameOver()
    {
        GameOverUI.SetActive(true);
        CrosshairUI.SetActive(false);
    }
    public void Pause()
    {
        // if not paused
        if (Time.timeScale == 1)
        {
            // pause and activate pause menu
            Time.timeScale = 0;
            PauseMenuUI.SetActive(true);
        }
        else
        {
            // if paused, resume game and remove pause menu
            Time.timeScale = 1;
            PauseMenuUI.SetActive(false);
        }
    }

    public void Restart()
    {
        // Reset player elements if necessary and set time and UI properly
        Time.timeScale = 1;
        PauseMenuUI.SetActive(false);
        GameOverUI.SetActive(false);
        SceneManager.LoadScene("Main");
    }

    public void Quit()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    public void TitleScreen()
    {
        SceneManager.LoadScene("Title Screen");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject TutorialMenuUI1;
    public GameObject TutorialMenuUI2;
    public GameObject TutorialMenuUI3;

    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        PauseGame.isPaused = false;
        PauseGame.isGameOver = false;
        PauseGame.isWin = false;
    }

    public void Start()
    {
        MainMenu.SetActive(true);
        TutorialMenuUI1.SetActive(false);

    }

    public void Tutorial()
    {
        MainMenu.SetActive(false);
        TutorialMenuUI1.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void TutorialMenu1Back()
    {
        TutorialMenuUI1.SetActive(false);
        MainMenu.SetActive(true);
    }
    public void TutorialMenu2Back()
    {
        TutorialMenuUI2.SetActive(false);
        TutorialMenuUI1.SetActive(true);
    }
    public void TutorialMenu3Back()
    {
        TutorialMenuUI3.SetActive(false);
        TutorialMenuUI2.SetActive(true);
    }
    public void TotorialMenu1Next()
    {
        TutorialMenuUI1.SetActive(false);
        TutorialMenuUI2.SetActive(true);
    }
    public void TotorialMenu2Next()
    {
        TutorialMenuUI2.SetActive(false);
        TutorialMenuUI3.SetActive(true);
    }
    public void TotorialMenu3Next()
    {
        TutorialMenuUI3.SetActive(false);
        MainMenu.SetActive(true);
    }
}

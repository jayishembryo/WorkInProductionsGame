using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject BackButton;
    [SerializeField]
    private GameObject HowToPlayScreen;
    [SerializeField]
    private GameObject PauseScreen;
    [SerializeField]
    private GameObject SettingsScreen;
    [SerializeField] private GameObject OpeningCanvas;

   [SerializeField] private GameObject CreditsCanvas;
   

    public void LoadHowToPlay()
    {
        HowToPlayScreen.SetActive(true);
        PauseScreen.SetActive(false);
    }

    public void GoBackToPause()
    {
        PauseScreen.SetActive(true);
        HowToPlayScreen.SetActive(false);
        SettingsScreen.SetActive(false);
    }

    public void QuitTheGame()
    {
        print("quit");
        Application.Quit();
    }
    
    public void GoToMain()
    {
        print("going to main");
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToSettings()
    {
        PauseScreen.SetActive(false);
        SettingsScreen.SetActive(true);
    }

    public void GoToSettingsOnMain()
    {
        SettingsScreen.SetActive(true);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("PrototypeLevel");
    }

    public void BackToGame()
    {
        PauseScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1.0f;
    }

    public void CreditsScreen()
    {
        OpeningCanvas.SetActive(false);
        CreditsCanvas.SetActive(true);
    }

    public void CreditsToOpening()
    {
        CreditsCanvas.SetActive(false);
        SettingsScreen.SetActive(false);
        HowToPlayScreen.SetActive(false);
        OpeningCanvas.SetActive(true);
    }

}

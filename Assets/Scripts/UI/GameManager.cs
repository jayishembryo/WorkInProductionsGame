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
    private UIManager uiM;

    private void Start()
    {

    }

    // Edit: 10/18/24 - Jacob - This code is goofy as hell; idk who worked on it
    // previously but its kinda not based. gotta update this soon.

    // MM -> HTP
    // IN-GAME -> HTP
    public void LoadHowToPlay()
    {
        OpeningCanvas.SetActive(false);
        HowToPlayScreen.SetActive(true);
        if (PauseScreen != null)
        {
            PauseScreen.SetActive(false);
        }
    }

    // HTP -> Pause
    // Settings -> Pause
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
    
    // INSERT SCENE HERE -> Main Menu
    public void GoToMain()
    {
        print("going to main");
        SceneManager.LoadScene("MainMenu");
    }

    // Pause -> Settings
    public void GoToSettings()
    {
        PauseScreen.SetActive(false);
        SettingsScreen.SetActive(true);
    }

    // MM -> Settings
    public void GoToSettingsOnMain()
    {
        OpeningCanvas.SetActive(false);
        SettingsScreen.SetActive(true);
    }

    // Scene -> In-Game
    public void PlayGame()
    {
        SceneManager.LoadScene("VerticalSlice");
    }

    // Pause -> In-Game
    public void BackToGame()
    {
        PauseScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1.0f;
    }

    // ???
    public void CreditsScreen()
    {
        OpeningCanvas.SetActive(false);
        CreditsCanvas.SetActive(true);
    }

    // Any MM UI -> MM
    public void CreditsToOpening()
    {
        CreditsCanvas.SetActive(false);
        SettingsScreen.SetActive(false);
        HowToPlayScreen.SetActive(false);
        OpeningCanvas.SetActive(true);

        // Jacob Note: Needs to be here to ensure animations continue to
        // work. Make sure main menu is index 0. Needs to be after main menu
        // canvas is enabled!
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            FindObjectOfType<UIManager>().PlayMainMenuAnimations();
        }
    }

}

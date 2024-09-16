using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreboardManager : MonoBehaviour
{

    public static ScoreboardManager Instance;

    [SerializeField]
    string scorePreamble;

    [SerializeField]
    int maxTime;

    [SerializeField]
    TMP_Text timer;

    [SerializeField]
    TMP_Text scoreboard;

    [SerializeField]
    TMP_Text gameOver;

    [SerializeField]
    UnityEvent start;

    [SerializeField]
    UnityEvent stop;

    [SerializeField]
    private GameObject mainMenuButton;

    int time;

    int score = 0;

    bool isRunning = false;

    public bool GameIsRunning = true;

    // Start is called before the first frame update
    void Start()
    {
        GameIsRunning = true;
        Instance = this;

        time = maxTime;
        gameOver.enabled = false;

        StartGame();
    }

    public void StartGame()
    {
        score = 0;
        /*if (isRunning)
            return;*/

        time = maxTime;
        gameOver.enabled = false;

        isRunning = true;

        start.Invoke();

       // StartCoroutine(TimeKeeper());
    }

    public void StopGame()
    {
        
        if (!isRunning)
            return;

        stop.Invoke();
        gameOver.enabled = true;
        mainMenuButton.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0.0f;
        GameIsRunning = false;
        isRunning = false;


    }

    IEnumerator TimeKeeper()
    {

        do
        {
            yield return new WaitForSeconds(1);
            time--;
            timer.text = "" + time;
        } while (time > 0);

        timer.faceColor = Color.red;

      //  StopGame();
    }

    public void AddScore(int amt)
    {
        score += amt;

        scoreboard.text = scorePreamble + score;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eater : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Maximum amount of food that can be stored")]
    float maxFeedBar;

    [SerializeField]
    [Tooltip("Starting hunger rate")]
    float startHunger;

    [SerializeField]
    [Tooltip("Rate at which hunger increases per second")]
    float hungerScalar;

    float currentFeedBar;

    float currentHunger;

    bool hasStarted = false;

    private void Start()
    {
        ResetHunger();
        StartHunger();
    }

    private void FixedUpdate()
    {
        if (!hasStarted)
            return;

        if (IsStarving())
            return;

        currentFeedBar -= currentHunger * Time.fixedDeltaTime;

        if (IsStarving())
        {
            OnStomachEmpty();
            return;
        }

        currentHunger += hungerScalar * Time.fixedDeltaTime;
    }

    private void OnStomachEmpty()
    {
        //Debug.Log("grumble grumble");
        ScoreboardManager.Instance.StopGame();
    }

    public void AddFood(float amt)
    {
        float newFeedAmt = amt + currentFeedBar;
        currentFeedBar = newFeedAmt > maxFeedBar ? maxFeedBar : newFeedAmt;
    }

    public bool IsStarving()
    {
        return currentFeedBar <= 0;
    }

    public float FeedMeter()
    {
        return currentFeedBar / maxFeedBar;
    }

    public void ResetHunger()
    {
        currentFeedBar = maxFeedBar;
        currentHunger = startHunger;
    }

    public void StartHunger()
    {
        hasStarted = true;
    }
}

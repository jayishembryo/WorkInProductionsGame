using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    [SerializeField]
    Transform player;

    [SerializeField]
    Eater playerEater;

    //[SerializeField]
    //PlayerController playerController;

    [SerializeField]
    float maxTime;

    private float remainingTime;

    private void Start()
    {
        Instance = this;

        remainingTime = maxTime;
    }

    public Transform GetPlayerTransform()
    {
        return player;
    }

    public float GetRemainingTime()
    {
        return remainingTime;
    }

    public void AddFood(float amt)
    {
       // playerEater.AddFood(amt);
    }
}

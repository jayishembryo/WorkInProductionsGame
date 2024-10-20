using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    PlayerController player;

    private void Start()
    {
        player = transform.parent.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        player.SetTouchedGround(true);
    }

    private void OnTriggerExit(Collider other)
    {
        player.SetTouchedGround(false);
    }
}

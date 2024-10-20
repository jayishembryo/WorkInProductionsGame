using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFadeToBlack : MonoBehaviour
{
    public GameObject FadeToBlackScren;
    private void OnTriggerEnter(Collider collision)
    {

        if (collision.transform.tag == "Player")
        {
            FadeToBlackScren.SetActive(true);
        }

    }
}

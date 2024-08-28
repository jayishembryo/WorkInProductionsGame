using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Feedbar : MonoBehaviour
{
    [SerializeField]
    Eater consoomer;

    [SerializeField]
    Image fill;

    private void FixedUpdate()
    {
        float fillAmt = consoomer.FeedMeter();
        //Debug.Log("Feed: " + fillAmt);
        fill.fillAmount = fillAmt;
    }
}

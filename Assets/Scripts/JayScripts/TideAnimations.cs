using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TideAnimations : MonoBehaviour
{
    Animator boat;

    public void Start()
    {

        boat = GetComponent<Animator>();

    }

    //anims called by animation events
    public void RiseAnim()
    {

        boat.SetTrigger("Rise");

    }

    public void FallAnim()
    {

        boat.SetTrigger("Fall");

    }

    public void EndRisingAnim()
    {

        if (GameObject.FindObjectOfType<EnvironmentalEffects>().TideActive == false)
        {

            boat.SetTrigger("Still");

        }
        else if (GameObject.FindObjectOfType<EnvironmentalEffects>().TideActive == true)
        {

            RiseAnim();

        }

    }

}

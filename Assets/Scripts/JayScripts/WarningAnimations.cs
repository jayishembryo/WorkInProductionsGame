using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningAnimations : MonoBehaviour
{

    bool animEnded = false;

    public void EndAnimation()
    {

        if(GameObject.FindObjectOfType<EnvironmentalEffects>().FireActive && !animEnded)
        {

            GameObject.FindObjectOfType<EnvironmentalEffects>().FireWarnings.SetActive(false);
            GameObject.FindObjectOfType<EnvironmentalEffects>().FireSpawn.SetActive(true);
            animEnded = true;

        }

        if(GameObject.FindObjectOfType<EnvironmentalEffects>().IceActive && !animEnded)
        {

            GameObject.FindObjectOfType<EnvironmentalEffects>().IceWarnings.SetActive(false);
            GameObject.FindObjectOfType<EnvironmentalEffects>().IceSpawn.SetActive(true);
            animEnded = true;

        }

    }

}

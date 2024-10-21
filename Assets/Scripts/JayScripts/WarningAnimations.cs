using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningAnimations : MonoBehaviour
{

    bool animEnded = false;

    public void EndAnimation()
    {

        if(GameObject.FindObjectOfType<EnvironmentalEffects>().FireActive)
        {

            if(this.tag == "First")
            {

                GameObject.FindObjectOfType<EnvironmentalEffects>().FireWarnings.SetActive(false);
                GameObject.FindObjectOfType<EnvironmentalEffects>().FireSpawn.SetActive(true);

            }
            

        }

        if(GameObject.FindObjectOfType<EnvironmentalEffects>().IceActive)
        {

            if (this.tag == "First")
            {

                GameObject.FindObjectOfType<EnvironmentalEffects>().IceWarnings.SetActive(false);
                GameObject.FindObjectOfType<EnvironmentalEffects>().IceSpawn.SetActive(true);


            }

        }

    }

    public void BossImminent()
    {

        //CALL BOSS HERE YIPPEE

    }

}

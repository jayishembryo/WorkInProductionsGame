using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class EnvironmentalEffects : MonoBehaviour
{

    int environment;
    private List<int> alreadyUsed = new List<int>();

    public void Decide()
    {

        //make the max value higher as we add more environmental effects
        environment = Random.Range(0, 0);

        if(alreadyUsed.Contains(environment))
        {

            Reroll();

        }
        else
        {

            if(environment == 0)
            {

                FlamesOfDisaster();
                alreadyUsed.Add(environment);

            }

        }

    }

    void Reroll()
    {

        //max value can still be changed
        if(alreadyUsed.Count > 0)
        {

            //end game here and/or call boss i think

        }
        else
        {

            Decide();

        }

    }

    //the one we need for the vertical slice
    void FlamesOfDisaster()
    {



    }

}

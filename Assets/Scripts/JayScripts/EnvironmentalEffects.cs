using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class EnvironmentalEffects : MonoBehaviour
{
    int environment;
    //change to match # of environments designed
    int allEnvironments =  0;

    private List<int> alreadyUsed = new List<int>();

    //temporary fix
    public GameObject FireSpawn;
    public GameObject IceSpawn;

    bool fireActive = false;
    bool iceActive = false;
    bool tideActive = false;
    bool beamsActive = false;

    private void Update()
    {
       
    }

    public void Decide()
    {

        environment = Random.Range(0, allEnvironments += 1);
        
        if(alreadyUsed.Contains(environment))
        {

            Reroll();
            return;

        }

        if(environment == 0)
        {

            FlamesOfDisaster();
            alreadyUsed.Add(environment);

        }
        if(environment == 1)
        {



        }
        if(environment == 2)
        {



        }
        if(environment == 3)
        {



        }
        
        //ADD OTHER ENVIRONMENT FUNCTIONS

    }

    void Reroll()
    {

        if (alreadyUsed.Count >= allEnvironments)
        {

            //call either the end of the game or boss fight here

        }
        else
        {

            Decide();

        }

    }

    //the one we need for the vertical slice
    public void FlamesOfDisaster()
    {

        fireActive = true;
        FireSpawn.SetActive(true);

    }

    public void TideRising()
    {



    }

    public void BoatFreezes()
    {

        iceActive = true;
        IceSpawn.SetActive(true);

    }

    public void EnergyBeams()
    {



    }

    public void ResetShip()
    {

        if(fireActive)
        {

            FireSpawn.SetActive(false);
            fireActive = false;

        }
        if(iceActive)
        {

            IceSpawn.SetActive(false);
            iceActive = false;

        }

    }

}

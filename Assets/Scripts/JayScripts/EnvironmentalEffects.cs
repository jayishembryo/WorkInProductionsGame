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
    public GameObject Puddle1;
    public GameObject Puddle2;
    public GameObject Puddle3;
    public GameObject Puddle4;
    public GameObject Puddle5;
    public GameObject Puddle6;

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

        Puddle1.SetActive(true);
        Puddle2.SetActive(true);
        Puddle3.SetActive(true);
        Puddle4.SetActive(true);
        Puddle5.SetActive(true);
        Puddle6.SetActive(true);

    }

}

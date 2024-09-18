using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class EnvironmentalEffects : MonoBehaviour
{
    public GameObject threatPrefab;
    public Transform[] positions;
    
    int environment;
    private List<int> alreadyUsed = new List<int>();

    public float decideInterval = 60f;
    private float timer;

    //temporary fix
    public GameObject Puddle1;
    public GameObject Puddle2;
    public GameObject Puddle3;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= decideInterval)
        {
            timer = 0f;
            Decide();
        }
    }

    public void Decide()
    {

        //make the max value higher as we add more environmental effects
        if (positions.Length == 0)
        {
            return;
        }

        if(alreadyUsed.Count >= (positions.Length))
        {
            Reroll();
            return;
        }

        int environment;
        do
        {
            environment = Random.Range(0, positions.Length);
        }
        while (alreadyUsed.Contains(environment));

       // FlamesOfDisaster(environment);
        alreadyUsed.Add(environment);

    }

    void Reroll()
    {

        //max value can still be changed
        if(alreadyUsed.Count >= positions.Length)
        {

            //end game here and/or call boss i think

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

    }

}

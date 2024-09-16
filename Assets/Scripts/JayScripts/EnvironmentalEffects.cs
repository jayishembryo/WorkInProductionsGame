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

        FlamesOfDisaster(environment);
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
    void FlamesOfDisaster(int positionIndex)
    {
        if (positions.Length > positionIndex)
        {
            Instantiate(threatPrefab, positions[positionIndex].position, Quaternion.identity);
        }
    }

}

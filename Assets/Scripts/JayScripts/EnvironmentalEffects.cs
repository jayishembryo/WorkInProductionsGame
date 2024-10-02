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

    public List<GameObject> beams = new List<GameObject>();
    public List<GameObject> warnings = new List<GameObject>();

    int lastBeam;

    private void Update()
    {
       
        while(iceActive && GameObject.FindObjectOfType<PlayerController>().IsTouchingGround == true)
        {

            //Debug.Log("weeeeeeeeeeeeeeee");
            GameObject.FindObjectOfType<PlayerController>().physicMaterial.dynamicFriction = 0;
            //GameObject.FindObjectOfType<PlayerController>().physicMaterial.staticFriction = 0;

        }

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

            TideRising();

        }
        if(environment == 2)
        {

            BoatFreezes();

        }
        if(environment == 3)
        {

            EnergyBeams();

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

        beamsActive = true;
        StartCoroutine(BeamsActivated());

    }

    IEnumerator BeamsActivated()
    {

        while (beamsActive)
        {
            //randomizes beam/warning called
            int currentBeam = Random.Range(0, 4);

            //makes sure that it doesn't call the same beam twice in a row
            if (currentBeam == lastBeam)
            {

                break;

            }

            lastBeam = currentBeam;

            warnings[currentBeam].SetActive(true);

            //warning should last for x amount of seconds before the beam is called
            yield return new WaitForSeconds(3);

            warnings[currentBeam].SetActive(false);

            beams[currentBeam].SetActive(true);

            yield return new WaitForSeconds(3);

            beams[currentBeam].SetActive(false);

            //loops
            yield return new WaitForSeconds(5);

        }

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
            if(GameObject.FindObjectOfType<PlayerController>().IsTouchingGround == true)
            {

                GameObject.FindObjectOfType<PlayerController>().physicMaterial.dynamicFriction = 0.6f;

            }

        }
        if(beamsActive)
        {

            beamsActive = false;

        }
        if(tideActive)
        {

            tideActive = false;

        }

    }

}

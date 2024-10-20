using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;

public class EnvironmentalEffects : MonoBehaviour
{
    public int Environment;
    //change to match # of environments designed
   // int allEnvironments =  3;

    private List<int> alreadyUsed = new List<int>();

    //temporary fix
    public GameObject FireSpawn;
    public GameObject IceSpawn;
    public GameObject TideSpawn;

    public GameObject FireWarnings;
    public GameObject IceWarnings;

    public Material IceFloor;
    public Material ShipFloor;

    public bool FireActive = false;
    public bool IceActive = false;
    public bool TideActive = false;
    bool beamsActive = false;

    public List<GameObject> beams = new List<GameObject>();
    public List<GameObject> warnings = new List<GameObject>();
    public List<GameObject> totems = new List<GameObject>();
    //public List<MeshRenderer> floorTiles = new List<GameObject>();

    public GameObject ActiveTotem;
    public bool TotemIsActive = false;

    int lastBeam;

    void Start()
    {

    }

    private void Update()
    {
       

    }

    public void Decide()
    {

        Environment = Random.Range(0, 4);
        
        if(alreadyUsed.Contains(Environment))
        {

            Reroll();
            return;

        }

        totems[Environment].SetActive(true);
        totems[Environment] = ActiveTotem;
        TotemIsActive = true;
        alreadyUsed.Add(Environment);

        //if(environment == 0)
        //{

        //    FlamesOfDisaster();
        //    alreadyUsed.Add(environment);
        //    environment = Random.Range(0, allEnvironments += 1);

        //}
        //if(environment == 1)
        //{

        //    TideRising();
        //    alreadyUsed.Add(environment);
        //    environment = Random.Range(0, allEnvironments += 1);

        //}
        //if(environment == 2)
        //{

        //    BoatFreezes();
        //    alreadyUsed.Add(environment);
        //    environment = Random.Range(0, allEnvironments += 1);

        //}
        //if(environment == 3)
        //{

        //    EnergyBeams();
        //    alreadyUsed.Add(environment);
        //    environment = Random.Range(0, allEnvironments += 1);

        //}
        
        //ADD OTHER ENVIRONMENT FUNCTIONS

    }

    void Reroll()
    {

        Decide();

    }

    //the one we need for the vertical slice
    public void FlamesOfDisaster()
    {

        FireActive = true;
        //FireSpawn.SetActive(true);
        FireWarnings.SetActive(true);

    }

    public void TideRising()
    {

        TideActive = true;
        //RiseAnim();
        GameObject.FindObjectOfType<TideAnimations>().RiseAnim();
        TideSpawn.SetActive(true);

    }

    public void BoatFreezes()
    {

        GameObject[] floors = GameObject.FindGameObjectsWithTag("Ground");

        for (int i = 0; i < floors.Length; i++)
        {

            floors[i].GetComponent<MeshRenderer>().material = IceFloor;

        }

        IceActive = true;
        //IceSpawn.SetActive(true);
        IceWarnings.SetActive(true);
        GameObject.FindObjectOfType<PlayerController>().physicMaterial.dynamicFriction = 0;

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

            //REMINDER: ADD ANIM TO WARNING
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

        if(TotemIsActive)
        {

            
            ActiveTotem.SetActive(false);

        }

        if(FireActive)
        {

            FireSpawn.SetActive(false);
            FireActive = false;

        }
        if(IceActive)
        {

            IceSpawn.SetActive(false);
            IceActive = false;
            GameObject.FindObjectOfType<PlayerController>().physicMaterial.dynamicFriction = 0.6f;
            GameObject[] floors = GameObject.FindGameObjectsWithTag("Ground");

            for (int i = 0; i < floors.Length; i++)
            {

                floors[i].GetComponent<MeshRenderer>().material = ShipFloor;

            }

        }
        if(beamsActive)
        {

            beamsActive = false;

        }
        if(TideActive)
        {

            TideActive = false;
            GameObject.FindObjectOfType<TideAnimations>().FallAnim();
            TideSpawn.SetActive(false);

        }

        Decide();

    }

}

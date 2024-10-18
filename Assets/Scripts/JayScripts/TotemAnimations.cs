using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemAnimations : MonoBehaviour
{

    //CALLS ENVIRONMENTAL EFFECTS

    private EnvironmentalEffects callEnvironment;
    private SpawnManager callSpawn;

    public void Start()
    {

        callEnvironment = GameObject.FindObjectOfType<EnvironmentalEffects>();
        callSpawn = GameObject.FindObjectOfType<SpawnManager>();

    }

    public void CallEnvironment()
    {

        callSpawn.NewWaveStartTotem();

        switch(callEnvironment.Environment)
        {

            case 0:
                callEnvironment.FlamesOfDisaster();
                break;
            case 1:
                callEnvironment.TideRising();
                break;
            case 2:
                callEnvironment.BoatFreezes();
                break;
            case 3:
                callEnvironment.EnergyBeams();
                break;

        }

    }

    public void NewEnvironment()
    {

        if(callSpawn.NoMoreSpawn == false)
        {

            callEnvironment.ResetShip();

        }

    }

}

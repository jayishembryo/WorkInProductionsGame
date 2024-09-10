using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private int waveNumber = 1;
    private int totalEnemies;
    private int totalEnemiesRemaining;
    private int numberOfNormals;//medium dude spawns allowed this wave.
    private int numberOfTanks;//large dude spawns allowed this wave.
    private int numberOfStingers;//flying dude spawns allowed this wave.
    [SerializeField]
    private float groupCooldown = 30.0f;//time since last group spawned.
    public float waveTime;//time since this wave started.
    private bool endSignal = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(endSignal)
        {
            newWaveStart();
        }
    }
    
    private void newWaveStart()
    {
        waveTime = Time.deltatime;//update start of current wave
        waveNumber++;//change wave start   
             
    }

    private void GroupAssignment()
    {

    }

    public void enemyHasDied(GameObject enemy)//when an enemy dies it reduces the counter for it's type that can spawn that wave as well as the total number of dudes
    {
        totalEnemies--;
        if(enemy.name == "normal") numberOfNormals--;

        if(enemy.name == "tank") numberOfTanks--;

        if(enemy.name == "stinger") numberOfStingers--;

        if(totalEnemies =< 0)
        {
            StartCoroutine(endWave());
        }

    }

    private IEnumerator endWave()
    {
        //stuff that happens as the wave ends goes here
        //like play a noise or a phase shift

        yield return new WaitForSeconds(5);

        endSignal = true;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private int waveNumber = 1;
    [SerializeField]
    private int totalEnemies = 10;
    private int totalEnemiesRemaining;
    [SerializeField]
    private int numberOfNormals;//medium dude spawns allowed this wave.
    [SerializeField]
    private int numberOfTanks;//large dude spawns allowed this wave.
    [SerializeField]
    private int numberOfStingers;//flying dude spawns allowed this wave.
    
    [SerializeField]
    private float groupCooldown = 30.0f;//time since last group spawned.
    private float groupTime;

    private float waitTime;
    public float waveTime;//time since this wave started.
    private bool endSignal = false;
    
    public GameObject[] spawnPoints;

    [SerializeField]
    private GameObject normal;
    [SerializeField]
    private GameObject tank;
    [SerializeField]
    private GameObject stinger;

    private int normNumber;
    private int tankNumber;
    private int stingNumber;
    // Start is called before the first frame update
    void Start()
    {
        newWaveStart();
    }

    // Update is called once per frame
    void Update()
    {
        if(endSignal)
        {
            newWaveStart();
            
        }

        if(waitTime >= groupTime)
        {
            GroupAssignment();
            groupTime = Time.deltaTime;
            waitTime = groupTime + groupCooldown;
        }
    }
    
    private void newWaveStart()
    {
        waveTime = Time.deltaTime;//update start of current wave
        groupTime = waveTime;
        waveNumber++;//change wave start 
        endSignal = false;
        
    }

    private void GroupAssignment()
    {
        if(numberOfNormals > 6)
        {
            normNumber = Random.Range(4, 8);
        }
        else normNumber = numberOfNormals;

        if(numberOfTanks > 2)
        {
        tankNumber = Random.Range(0, 3);
        }
        else tankNumber = numberOfTanks;

        if(numberOfStingers > 4)
        {
        stingNumber = Random.Range(0, 5);
        }
        else stingNumber = numberOfStingers;// spawns max nymber of remaining stingers


        for (int y = 0; y < spawnPoints.Length; y++)//shuffles through spawn points for spawning enemies
        {
            Vector3 spawnPicked = spawnPoints[y].transform.position;//sets the chosen spawn point for spawning enemies

            for (int x = 0; x < normNumber; x++)
            {
                Instantiate(normal, spawnPicked, Quaternion.identity);//spawns normal dudes
                numberOfNormals -= normNumber;
            }

            if(tankNumber > 0)
            {
                for (int x = 0; x < tankNumber; x++)
                {  
                    //Instantiate(tank, spawnPicked, Quaternion.identity);//spawns tanks
                    //numberOfTanks -= tankNumber;
                }
            }
            if(stingNumber > 0)
            {
                for (int x = 0; x < stingNumber; x++)
                {
                    //Instantiate(stinger, spawnPicked, Quaternion.identity);//spawns stingers
                    //numberOfStingers -= stingNumber;
                }
            }
        }
    }

    public void enemyHasDied(GameObject enemy)//when an enemy dies it reduces the counter for it's type that can spawn that wave as well as the total number of dudes
    {
        totalEnemies--;
        if(enemy.name == "normal") numberOfNormals--;

        if(enemy.name == "tank") numberOfTanks--;

        if(enemy.name == "stinger") numberOfStingers--;

        if(totalEnemies <= 0)
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

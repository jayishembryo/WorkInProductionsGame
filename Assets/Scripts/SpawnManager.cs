using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private int waveNumber;
    int maxWaveNumber = 2;
    [SerializeField]
    public int TotalEnemies;
    private int totalEnemiesRemaining;


    [SerializeField]
    private int numberOfNormals;//medium dude spawns allowed this wave.
    [SerializeField]
    private int numberOfTanks;//large dude spawns allowed this wave.
    [SerializeField]
    private int numberOfStingers;//flying dude spawns allowed this wave.
    
    [SerializeField]
    private float groupCooldown;//time since last group spawned.
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

    [SerializeField]
    private List<WaveOrganizer> waveOrganizer;
    

    [SerializeField]
    private int normNumber;
    //private int minNormalPerGroup;
    //private int maxNormalPerGroup;
    [SerializeField]
    private int tankNumber;
    //private int minTankPerGroup;
    //private int maxTankPerGroup;
    [SerializeField]
    private int stingNumber;
    //private int minStingerPerGroup;
    //private int maxStingerPerGroup;


    public GameObject NewWaveTextBox;
    public TMP_Text NewWave;

    private int nextSpawnPoint = 0;
    // Start is called before the first frame update
    void Start()
    {
        newWaveStart();
    }

    // Update is called once per frame
    void Update()
    {
        
        groupTime = Time.time;

       if(groupTime >= waitTime)// the cooldown between bursts of enemies
       {
            GroupAssignment();
            waitTime = groupTime + groupCooldown;
       }
        
    }
    
    public void newWaveStart()
    {

        //waveTime = Time.time;//update start of current wave
       // groupTime = waveTime;
        waveNumber++;
        StartCoroutine(NewWaveText());

        groupCooldown = waveOrganizer[waveNumber].groupCooldown;
        numberOfNormals = waveOrganizer[waveNumber].totalNormals;
        numberOfTanks = waveOrganizer[waveNumber].totalTanks;
        numberOfStingers = waveOrganizer[waveNumber].totalStingers;

        GroupAssignment();
        TotalEnemies = GameObject.FindObjectsOfType<EnemyBehaviour>().Length;
        //endSignal = false;
        
        if (waveNumber > 1)
        {

            FindObjectOfType<EnvironmentalEffects>().Decide();

        }

        if (waveNumber <= maxWaveNumber)
        {

            return;

        }
        else if (waveNumber > maxWaveNumber)
        {
           // Debug.Log("HUH");
            ScoreboardManager.Instance.YouWin();

        }

    }

    private void GroupAssignment()
    {
        if(numberOfNormals > waveOrganizer[waveNumber].maxNormalsInGroup - 1)//if the number of normals is greater than the spawn amount
        {
            normNumber = Random.Range(waveOrganizer[waveNumber].minNormalsInGroup, waveOrganizer[waveNumber].maxNormalsInGroup);//set it to spawn a few of them. should set variables for max and minimum based on wave.
        }
        else normNumber = numberOfNormals;//spawn the remainder of normals

        if(numberOfTanks > waveOrganizer[waveNumber].maxTanksInGroup - 1)//if the number of tank is greater than the spawn amount
        {
            tankNumber = Random.Range(waveOrganizer[waveNumber].minTanksInGroup, waveOrganizer[waveNumber].maxTanksInGroup);//set it to spawn a few of them. should set variables for max and minimum based on wave.
        }
        else tankNumber = numberOfTanks;//spawn the remainder of tanks

        if(numberOfStingers > waveOrganizer[waveNumber].maxStingersInGroup - 1)//if the number of stingers is greater than the spawn amount
        {
            stingNumber = Random.Range(waveOrganizer[waveNumber].minStingersInGroup, waveOrganizer[waveNumber].maxStingersInGroup);//set it to spawn a few of them. should set variables for max and minimum based on wave.
        }
        else stingNumber = numberOfStingers;// spawns max number of remaining stingers


        //TAKE THE STUFF FROM ABOVE HERE TO SPAWN A NUMBER OF DUDES
        
        if(normNumber > 0)
        {
            for (int x = 0; x < normNumber; x++)
            {
                Vector3 spawnPicked = spawnPoints[nextSpawnPoint].transform.position;//sets the chosen spawn point for spawning enemies
                Instantiate(normal, spawnPicked, Quaternion.identity);//spawns normal dudes
                nextSpawnPoint = (nextSpawnPoint + 1) % spawnPoints.Length;//makes sure the next spawn point spawns enemies more spread out
            }
            numberOfNormals -= normNumber;
        }

        if(tankNumber > 0)
        {
            for (int x = 0; x < tankNumber; x++)
            {  
                Vector3 spawnPicked = spawnPoints[nextSpawnPoint].transform.position;//sets the chosen spawn point for spawning enemies
                Instantiate(tank, spawnPicked, Quaternion.identity);//spawns tanks
                nextSpawnPoint = (nextSpawnPoint + 1) % spawnPoints.Length;//makes sure the next spawn point spawns enemies more spread out   
            }
            numberOfTanks -= tankNumber;
        }
        if(stingNumber > 0)
        {
            for (int x = 0; x < stingNumber; x++)
            {
                Vector3 spawnPicked = spawnPoints[nextSpawnPoint].transform.position;//sets the chosen spawn point for spawning enemies
                Instantiate(stinger, spawnPicked, Quaternion.identity);//spawns stingers
                nextSpawnPoint = (nextSpawnPoint + 1) % spawnPoints.Length;//makes sure the next spawn point spawns enemies more spread out
                
            }
            numberOfStingers -= stingNumber;
        }

        numberOfNormals = System.Math.Clamp(numberOfNormals - normNumber, 0, 100);

    }

    public IEnumerator NewWaveText()
    {

        NewWaveTextBox.SetActive(true);
        NewWave.text = "WAVE " + waveNumber.ToString() + " STARTED";

        yield return new WaitForSeconds(2);

        NewWaveTextBox.SetActive(false);

    }
}

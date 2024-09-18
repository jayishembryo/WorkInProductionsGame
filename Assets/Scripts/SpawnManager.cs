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

       // if(groupTime >= waitTime)// the cooldown between bursts of enemies
       // {
         //   GroupAssignment();
        //    waitTime = groupTime + groupCooldown;
       // }
        
    }
    
    private void newWaveStart()
    {

        waveTime = Time.time;//update start of current wave
        groupTime = waveTime;
        GroupAssignment();
        waveNumber++;
        StartCoroutine(NewWaveText());
        TotalEnemies = GameObject.FindObjectsOfType<EnemyBehaviour>().Length;
        //endSignal = false;

        if (waveNumber > 1)
        {

            FindObjectOfType<EnvironmentalEffects>().FlamesOfDisaster();

        }

        if (waveNumber <= maxWaveNumber)
        {

            return;

        }
        else if (waveNumber > maxWaveNumber)
        {

            ScoreboardManager.Instance.StopGame();

        }

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
        else stingNumber = numberOfStingers;// spawns max number of remaining stingers


        
        

        for (int x = 0; x < normNumber; x++)
        {
            Vector3 spawnPicked = spawnPoints[nextSpawnPoint].transform.position;//sets the chosen spawn point for spawning enemies
            Instantiate(normal, spawnPicked, Quaternion.identity);//spawns normal dudes
            nextSpawnPoint = (nextSpawnPoint + 1) % spawnPoints.Length;//makes sure the next spawn point spawns enemies more spread out
        }

        if(tankNumber > 0)
        {
            for (int x = 0; x < tankNumber; x++)
            {  
                Vector3 spawnPicked = spawnPoints[nextSpawnPoint].transform.position;//sets the chosen spawn point for spawning enemies
                Instantiate(tank, spawnPicked, Quaternion.identity);//spawns tanks
                nextSpawnPoint = (nextSpawnPoint + 1) % spawnPoints.Length;//makes sure the next spawn point spawns enemies more spread out   
            }
            //numberOfTanks -= tankNumber;
        }
        if(stingNumber > 0)
        {
            for (int x = 0; x < stingNumber; x++)
            {
                Vector3 spawnPicked = spawnPoints[nextSpawnPoint].transform.position;//sets the chosen spawn point for spawning enemies
                Instantiate(stinger, spawnPicked, Quaternion.identity);//spawns stingers
                nextSpawnPoint = (nextSpawnPoint + 1) % spawnPoints.Length;//makes sure the next spawn point spawns enemies more spread out
                
            }
            //numberOfStingers -= stingNumber;
        }

        numberOfNormals = System.Math.Clamp(numberOfNormals - normNumber, 0, 100);
    }

    public void StartEndWave()
    {

        StartCoroutine(endWave());

       // Destroy(enemy);

    }

    public IEnumerator endWave()
    {
        //stuff that happens as the wave ends goes here
        //like play a noise or a phase shift

        yield return new WaitForSeconds(1.5f);

        newWaveStart();

    }

    public IEnumerator NewWaveText()
    {

        NewWaveTextBox.SetActive(true);
        NewWave.text = "WAVE " + waveNumber.ToString() + " STARTED";

        yield return new WaitForSeconds(2);

        NewWaveTextBox.SetActive(false);

    }
}

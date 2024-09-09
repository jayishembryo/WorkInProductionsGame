using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private int waveNumber;
    private int totalEnemiesThisWave;
    private int numberOfNormals;//medium dude spawns allowed this wave.
    private int numberOfTanks;//large dude spawns allowed this wave.
    private int numberOfStingers;//flying dude spawns allowed this wave.
    public float groupCooldown = 30.0f;//time since last group spawned.
    public float waveTime;//time since this wave started.
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GroupAssignment()
    {

    }
}

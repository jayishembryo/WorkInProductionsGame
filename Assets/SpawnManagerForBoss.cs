using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerForBoss : MonoBehaviour
{
    public GameObject normalEnemy;
    public GameObject stingerEnemy;

    public Transform[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 7.1f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemy()
    {
        Vector3 spawnPicked = spawnPoints[Random.Range(0, 9)].position; // picks a random point 
        int choseEnemy = Random.Range(0, 6);
        if (choseEnemy <= 3)
        {
            Instantiate(normalEnemy, spawnPicked, Quaternion.identity);//spawns normal dudes
        }
        else
        {
            Instantiate(stingerEnemy, spawnPicked, Quaternion.identity);//spawns stinger dudes
        }
    }
}

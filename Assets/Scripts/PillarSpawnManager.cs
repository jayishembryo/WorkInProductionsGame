using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarSpawnManager : MonoBehaviour
{
    public float spawnRadius;
    public float pillarRate;
    public GameObject[] pillarObjects;

    void Start()
    {
        InvokeRepeating("SpawnPillarWithParameter", 0f, pillarRate);
    }


    private void SpawnPillarWithParameter()
    {
        SpawnPillar(-1);
    }
    /// <summary>
    /// Spawns a pillar. Input pillarID to spawn. -1 makes pillar random.
    /// </summary>
    public void SpawnPillar(int pillarID)
    {

        switch (pillarID)
        {
            case -1:
                int randomPillar = Random.Range(0, 5);
                Instantiate(pillarObjects[randomPillar], Random.insideUnitSphere * spawnRadius + transform.position, Quaternion.identity);
                break;
            default:
                Instantiate(pillarObjects[pillarID], Random.insideUnitSphere * spawnRadius + transform.position, Quaternion.identity);
                break;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}

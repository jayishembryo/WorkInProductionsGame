using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TideKill : MonoBehaviour
{
    
    public void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.CompareTag("Player"))
        {

            ScoreboardManager.Instance.StopGame();

        }
        if(collision.gameObject.CompareTag("Enemy"))
        {

            FindObjectOfType<SpawnManager>().TotalEnemies = GameObject.FindObjectsOfType<EnemyBehaviour>().Length;
            FindObjectOfType<SpawnManager>().TotalEnemies -= 1;

            if (FindObjectOfType<SpawnManager>().TotalEnemies <= 0 && FindObjectOfType<SpawnManager>().Waiting == false)
            {

                FindObjectOfType<SpawnManager>().Waiting = true;
                FindObjectOfType<SpawnManager>().newWaveStart();

            }

            Destroy(collision.gameObject);

        }

    }

}

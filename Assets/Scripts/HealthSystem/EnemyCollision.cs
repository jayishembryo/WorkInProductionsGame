using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{

    PlayerKnockback playerKnockbackInstance;

    private void Awake()
    {

        playerKnockbackInstance = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerKnockback>();

    }

    void OnTriggerStay(Collider other)
    {
        // When colliding with anything tagged "enemy", recieve damage.
        if(other.gameObject.CompareTag("Enemy"))
        {
            EnemyBehaviour otherEnemy = other.gameObject.GetComponent<EnemyBehaviour>();
            FindObjectOfType<PlayerController>().FireScreen.SetActive(true);
            if (otherEnemy != null)
            {
                HealthSystem.instance.Damage(otherEnemy.damageToPlayer);
                Debug.Log(otherEnemy.name + " has damaged player.");
            }

            if (playerKnockbackInstance.CanBeKnockedBack == true)
            {

                playerKnockbackInstance.CallKnockBack();

            }
            
        }
    }

    // Commented code here serves as a debugging source for health.
    // void Update()
    // {
    //     if(Input.GetKey(KeyCode.E))
    //     {
    //         HealthSystem.playerHealth -= enemyDamage;
    //     }
    //     if(Input.GetKey(KeyCode.B))
    //     {
    //         Debug.Log(HealthSystem.playerHealth);
    //     }
    // }
    
    
}

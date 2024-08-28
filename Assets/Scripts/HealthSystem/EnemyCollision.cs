using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    // Damage value set as SerialzeField for ease of access.
    [SerializeField] float enemyDamage = 10f;

    void OnTriggerStay(Collider other)
    {
        // When colliding with anything tagged "enemy", recieve damage.
        if(other.gameObject.CompareTag("Enemy"))
        {
            HealthSystem.instance.Damage(enemyDamage);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDOT : MonoBehaviour
{
    [SerializeField]
    private float damage;

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            HealthSystem.instance.FireDamage(damage);
        }
    }

}

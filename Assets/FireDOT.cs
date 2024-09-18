using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDOT : MonoBehaviour
{
    [SerializeField]
    private float damage = 1f;

    float whenToAddTime = 1;
    float lastAddedToTime = 0;

    public void FixedUpdate()
    {

        lastAddedToTime += Time.fixedDeltaTime;

    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            FindObjectOfType<PlayerController>().FireScreen.SetActive(true);

            if (lastAddedToTime > whenToAddTime)
            {

                HealthSystem.instance.FireDamage(damage);
                lastAddedToTime = 0;

            }

        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {

            FindObjectOfType<PlayerController>().FireScreen.SetActive(false);

        }

    }

}

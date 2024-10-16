using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{

    //put this on a beam prefab

    private void OnTriggerStay(Collider other)
    {
        
        if(other.gameObject.GetComponent<PlayerController>())
        {

            HealthSystem.instance.Damage(15);

            if(other.GetComponent<PlayerKnockback>().CanBeKnockedBack == true)
            {

                other.GetComponent<PlayerKnockback>().CallKnockBack();

            }

        }

    }

}

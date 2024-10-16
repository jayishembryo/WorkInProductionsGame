using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


//put this on an ice prefab!!

public class IcePatchScript : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        
        if(other.gameObject.GetComponent<PlayerController>())
        {

            HealthSystem.instance.maxKnockBack += 15;
            HealthSystem.instance.knockBack += 15;

            HealthSystem.instance.maxLaunch += 7.5f;
            HealthSystem.instance.launch += 7.5f;

        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.GetComponent<PlayerController>())
        {

            HealthSystem.instance.maxKnockBack -= 15;
            HealthSystem.instance.knockBack -= 15;

            HealthSystem.instance.maxLaunch -= 7.5f;
            HealthSystem.instance.launch -= 7.5f;

        }

    }

}

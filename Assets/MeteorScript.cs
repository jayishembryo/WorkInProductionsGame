using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{

    public GameObject fireObject;
    public GameObject explosionEffect;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(explosionEffect, gameObject.transform.position, Quaternion.identity);
        Instantiate(fireObject, new Vector3(gameObject.transform.position.x, 21f, gameObject.transform.position.z), Quaternion.identity);
        Destroy(gameObject);
    }
}

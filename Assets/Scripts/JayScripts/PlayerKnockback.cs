using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{

    Rigidbody rb;

    public PlayerController PlayerControllerInstance;

    public bool CanBeKnockedBack = true;
    public bool CanKick = true;

    void Start()
    {

        rb = GetComponent<Rigidbody>();

    }

    public void CallKnockBack()
    {

        StartCoroutine(KnockBack());

    }

    public IEnumerator KnockBack()
    {

        CanKick = false;

        rb.AddForce(transform.forward * -50f, ForceMode.Impulse);
        rb.AddForce(transform.up * 10f, ForceMode.Impulse);

        CanBeKnockedBack = false;


        yield return new WaitForSeconds(0.5f);

        CanKick = true;
        Invoke("Reactivation", 0.5f);


    }

    public void Reactivation()
    {

        CanBeKnockedBack = true;

    }

}

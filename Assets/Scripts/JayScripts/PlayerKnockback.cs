using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{

    Rigidbody rb;

    public PlayerController PlayerControllerInstance;

    public bool CanBeKnockedBack = true;

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

        rb.AddForce(transform.forward * -50f, ForceMode.Impulse);
        rb.AddForce(transform.up * 10f, ForceMode.Impulse);

        CanBeKnockedBack = false;

        yield return new WaitForSeconds(0.3f);

        CanBeKnockedBack = true;

    }

}

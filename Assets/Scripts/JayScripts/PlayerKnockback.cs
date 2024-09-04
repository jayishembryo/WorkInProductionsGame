using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{

    Rigidbody rb;

    public PlayerController PlayerControllerInstance;
    HealthSystem healthSystemInstance;

    public bool CanBeKnockedBack = true;
    public bool CanKick = true;

    float knockBackForce;
    float launchForce;

    void Start()
    {

        rb = GetComponent<Rigidbody>();
        healthSystemInstance = GameObject.FindGameObjectWithTag("Health").GetComponent<HealthSystem>();

    }

    public void CallKnockBack()
    {

        StartCoroutine(KnockBack());

    }

    public IEnumerator KnockBack()
    {

        CanKick = false;

        knockBackForce = 5f * (healthSystemInstance.knockBack / 10f);
        launchForce = 5f * (healthSystemInstance.launch / 10f);

        rb.AddForce(transform.forward * 50f, ForceMode.Impulse);
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

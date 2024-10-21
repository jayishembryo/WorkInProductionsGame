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

    [SerializeField] private Animator playermodelAnim;

    void Start()
    {

        rb = GetComponent<Rigidbody>();
        healthSystemInstance = GameObject.FindGameObjectWithTag("PlayerHitbox").GetComponent<HealthSystem>();

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

        rb.AddForce(transform.forward * -knockBackForce, ForceMode.Impulse);
        rb.AddForce(transform.up * launchForce, ForceMode.Impulse);

        CanBeKnockedBack = false;

        playermodelAnim.SetTrigger("hurt");

        yield return new WaitForSeconds(0.5f); //<--- should be scalable

        CanKick = true;
        Invoke("Reactivation", 0.5f);
        playermodelAnim.SetTrigger("hurtEnd");
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController.FireScreen != null)
        {

            playerController.FireScreen.SetActive(false);

        }

    }

    public void Reactivation()
    {

        CanBeKnockedBack = true;

    }

}

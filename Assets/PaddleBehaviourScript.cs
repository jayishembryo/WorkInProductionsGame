using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleBehaviourScript : MonoBehaviour
{
    private Animator anim;
    private PlayerController playerCon;
    [SerializeField] private bool rightSide;
    [SerializeField] private Transform bouncePosition;
    public bool bouncePreparation;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        playerCon = FindObjectOfType<PlayerController>();
        if (rightSide == true)
        {
            anim.SetFloat("idleSpeed", -1f);
        }
    }
    private void Update()
    {
        if (bouncePreparation == true)
        {
            playerCon.gameObject.transform.position = bouncePosition.position;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (rightSide)
            {
                case true:
                    StartCoroutine(playerCon.PaddleBounce(-1));
                    break;
                case false:
                    StartCoroutine(playerCon.PaddleBounce(1));
                    break;
            }
            anim.SetTrigger("bounce");
            Debug.Log("Player has collided with " + gameObject.name + "!");
            Invoke(nameof(ResetBounceTrigger), .6f);
        }
    }

    private void ResetBounceTrigger()
    {
        anim.ResetTrigger("bounce");
    }
    /*private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.ResetTrigger("bounce");
        }
    }*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    // Health bar implementation aided by tutorial by Tarodev tutorial

    // Getting access to the Slider function.

    public static HealthSystem instance;

    [SerializeField] public float maxRes = 100f;
    [SerializeField] public float playerRes;
    [SerializeField] public float maxKnockBack = 200f;
    [SerializeField] public float knockBack = 100f;
    [SerializeField] public float maxLaunch = 50f;
    [SerializeField] public float launch = 20f;
    [SerializeField] private Collider HitBox;
    [SerializeField] private bool isHit;
    [SerializeField] private float iFrameDuration = 1f;
  
  

    public Image HealthFill;
    [SerializeField] public Gradient HealthFillGradient;
    [SerializeField] private Animator faceAnimator;

    void Start()
    {
        instance = this;
        playerRes = 0;
        faceAnimator.SetFloat("Damage",playerRes);
 
    }

    // Update is called once per frame
    void Update()
    {
        faceAnimator.SetFloat("Damage",playerRes);
        // Function aided with 
        if(playerRes < 0f)
        {
            playerRes = 0f;
        }
        else if(playerRes > maxRes)
        {
            playerRes = maxRes;
        }
        
        // Divison here is done because value of fill is between 0 & 1.
        HealthFill.fillAmount = Mathf.Lerp(HealthFill.fillAmount,
            (playerRes / maxRes), 0.5f);

        HealthFill.color = HealthFillGradient.Evaluate(HealthFill.fillAmount);


    }

    public void Damage(float damage)
    {
        if (isHit)
        {
            return;
        }
        playerRes += damage;
        faceAnimator.SetFloat("Damage",playerRes);

        if (knockBack < maxKnockBack)
        {
            knockBack += damage;
        }

        if (knockBack > maxKnockBack)
        {
            knockBack = maxKnockBack;
        }

        if (launch < maxLaunch)
        {
            launch += (damage / 2f);
        }

        if (launch > maxLaunch)
        {
            launch = maxLaunch;
        }

        isHit = true;
        StartCoroutine(IFrames());
       
    }

    public void Heal(float heal)
    {

        playerRes -= heal;
        GameObject.FindObjectOfType<PlayerController>().HealthScreen.SetActive(true);
        StartCoroutine(Healed());
        

    }

    IEnumerator Healed()
    {

        yield return new WaitForSeconds(.5f);
        faceAnimator.SetFloat("Damage",playerRes);
        GameObject.FindObjectOfType<PlayerController>().HealthScreen.SetActive(false);
        yield return null;

    }

    IEnumerator IFrames()
    {
        yield return new WaitForSeconds(iFrameDuration);
        isHit = false;
    }

    public void FireDamage(float damage)
    {
        playerRes += damage;
        StartCoroutine(OnFire());
        Debug.Log("Player has taken fire damage");
    }

    IEnumerator OnFire()//turns on burning effect for player icon
    {
        faceAnimator.SetBool("Fire", true);
        yield return new WaitForSeconds(.5f);
        faceAnimator.SetBool("Fire", false);
        yield return null;
    }

}

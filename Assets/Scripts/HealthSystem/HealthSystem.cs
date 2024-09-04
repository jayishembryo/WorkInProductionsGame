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

    void Start()
    {
        instance = this;
        playerRes = maxRes;
    }

    // Update is called once per frame
    void Update()
    {
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

        
    }

    public void Damage(float damage)
    {
        if (isHit)
        {
            return;
        }
        playerRes -= damage;

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
            launch += (damage / 5f);
        }

        if (launch > maxLaunch)
        {
            launch = maxLaunch;
        }

        isHit = true;
        StartCoroutine(IFrames());
       
    }

    public void Heal(float amt)
    {
        float temp = playerRes + amt;
        if (temp > maxRes)
            temp = maxRes;

        playerRes = temp;
    }

    IEnumerator IFrames()
    {
        yield return new WaitForSeconds(iFrameDuration);
        isHit = false;
    }

}

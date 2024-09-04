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

    [SerializeField] public float maxHealth = 100f;
    [SerializeField] public float maxKnockBack = 200f;
    [SerializeField] public float knockBack = 100f;
    [SerializeField] public float playerHealth;
    [SerializeField] private Collider HitBox;
    [SerializeField] private bool isHit;
    [SerializeField] private float iFrameDuration = 3f;
  

    public Image HealthFill;

    void Start()
    {
        instance = this;
        playerHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Function aided with 
        if(playerHealth < 0f)
        {
            playerHealth = 0f;
        }
        else if(playerHealth > maxHealth)
        {
            playerHealth = maxHealth;
        }
        
        // Divison here is done because value of fill is between 0 & 1.
        HealthFill.fillAmount = Mathf.Lerp(HealthFill.fillAmount,
            (playerHealth / maxHealth), 0.5f);

        
    }

    public void Damage(float damage)
    {
        if (isHit)
        {
            return;
        }
        playerHealth -= damage;
        isHit = true;
        StartCoroutine(IFrames());
       
    }

    public void Heal(float amt)
    {
        float temp = playerHealth + amt;
        if (temp > maxHealth)
            temp = maxHealth;

        playerHealth = temp;
    }

    IEnumerator IFrames()
    {
        yield return new WaitForSeconds(iFrameDuration);
        isHit = false;
    }

}

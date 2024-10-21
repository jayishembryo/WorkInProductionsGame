using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class EnemyCollision : MonoBehaviour
{
    [SerializeField]
    private EventReference GroanSFX;
    private EventInstance GroanSFXInstance;

    PlayerKnockback playerKnockbackInstance;

    private void Awake()
    {

        playerKnockbackInstance = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerKnockback>();

        UpdateSoundPosition();
    }
    private void Update()
    {
        UpdateSoundPosition();
    }
    //SoundRelated
    public void PlayGroanSound()
    {
        PlaySound(ref GroanSFXInstance, GroanSFX);
    }
    private void PlaySound(ref EventInstance instance, EventReference eventRef)
    {
        instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        instance.release();
        instance = RuntimeManager.CreateInstance(eventRef);
        instance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        instance.start();
    }
    private void UpdateSoundPosition()
    {
        var attributes = RuntimeUtils.To3DAttributes(gameObject);
        GroanSFXInstance.set3DAttributes(attributes);
    }
    //End of Sound Related
    void OnTriggerStay(Collider other)
    {
        // When colliding with anything tagged "enemy", recieve damage.
        if(other.gameObject.CompareTag("Enemy"))
        {
            EnemyBehaviour otherEnemy = other.gameObject.GetComponent<EnemyBehaviour>();
            
            PlayerController playerController = FindObjectOfType<PlayerController>();
            if(playerController.FireScreen != null)
            {

                playerController.FireScreen.SetActive(true);

            }

            if (otherEnemy != null && GameObject.FindObjectOfType<PlayerController>().CanBeHit)
            {
                HealthSystem.instance.Damage(otherEnemy.damageToPlayer);
                Debug.Log(otherEnemy.name + " has damaged player.");
            }
            else
            {

                return;

            }

            if (playerKnockbackInstance.CanBeKnockedBack == true)
            {

                playerKnockbackInstance.CallKnockBack();
                PlayGroanSound();
            }
            
        }
    }

    // Commented code here serves as a debugging source for health.
    // void Update()
    // {
    //     if(Input.GetKey(KeyCode.E))
    //     {
    //         HealthSystem.playerHealth -= enemyDamage;
    //     }
    //     if(Input.GetKey(KeyCode.B))
    //     {
    //         Debug.Log(HealthSystem.playerHealth);
    //     }
    // }
    
    
}

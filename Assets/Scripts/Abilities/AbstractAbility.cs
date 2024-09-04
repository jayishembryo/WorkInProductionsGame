using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAbility : MonoBehaviour, IAbility
{
    string IAbility.Name => abilityName;

    float IAbility.CCD { get => currentCooldown; set => this.currentCooldown = value; }
    float IAbility.MaxCooldown { get => maxCooldown; set => this.maxCooldown = value; }
    float IAbility.CooldownRate { get => cooldownRate; set => this.cooldownRate = value; }

    [Header("Ability Stats")]
    [SerializeField]
    protected string abilityName = "";

    [SerializeField]
    [Tooltip("The cooldown for the ability. Current Cooldown is set to this when the ability is used.")]
    protected float maxCooldown = 1;

    [SerializeField]
    [Tooltip("Scalar for deltaTime ticking down for Current Cooldown")]
    protected float cooldownRate = 1;

    protected float currentCooldown = 0;

    PlayerKnockback playerKnockback;

    // Should be called via input event

    void Start()
    {

        playerKnockback = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerKnockback>();

    }

    public void OnAbilityTrigger()
    {
        if (currentCooldown > 0)
            return;

        if (playerKnockback.CanKick == true)
        {

            Execute();

        }

        TriggerCooldown();
    }

    // The actual function of the ability to be referenced from the child
    protected abstract void Execute();

    // If an ability requires a constant ticking effect, it can populate this method and use it
    protected abstract void ChildTick();

    // Called after ability is executed
    protected void TriggerCooldown()
    {
        currentCooldown = maxCooldown;
    }

    // Tracks the Current Cooldown and calls ChildTick
    public void Tick(float deltaTime)
    {
        if(currentCooldown > 0)
            currentCooldown -= deltaTime * cooldownRate;

        ChildTick();
    }

    private void FixedUpdate()
    {
        Tick(Time.fixedDeltaTime);
    }
}

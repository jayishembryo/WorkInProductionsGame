using System.Collections;
using System.Collections.Generic;

public interface IAbility
{
    public string Name { get; }

    public float CCD { get; set; }

    public float MaxCooldown { get; set; }

    public float CooldownRate { get; set; }

    public void OnAbilityTrigger();

    public void Tick(float deltaTime);
}

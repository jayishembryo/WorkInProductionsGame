using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class Dash : AbstractAbility
{
    [SerializeField]
    PlayerController player;

    [SerializeField]
    Rigidbody characterRB;

    [SerializeField]
    Transform rotationAnchor;

    [SerializeField]
    float forceScalar = 1;

    [SerializeField]
    ForceMode forceMode;

    [SerializeField]
    float upwardForce = 1;

    [SerializeField]
    float empoweredKickDuration;

    [SerializeField]
    float groundSpeed;

    [SerializeField]
    float speedLimitDelay;

    [SerializeField]
    float speedLimit;

    [SerializeField]
    MovementType movementType = MovementType.FACING_DIRECTION;

    float empoweredKick = 1;

    Vector3 upwardVector;

    delegate Vector3 MovementMethod();
    MovementMethod MovementDirection;

    /*[SerializeField]
    private EventReference DashSFX;
    private EventInstance DashSFXInstance;*/
    private void Start()
    {
        upwardVector = new(0, upwardForce, 0);

        switch (movementType)
        {
            case (MovementType.FACING_DIRECTION):
                MovementDirection = FacingDirection;
                return;
            case (MovementType.INPUT_DIRECTION):
                MovementDirection = InputDirection;
                return;
        }

        //DashSFXInstance = RuntimeManager.CreateInstance(DashSFX);
        //UpdateSoundPosition();
    }

   /* private void Update()
    {
        UpdateSoundPosition();
    }
    public void PlayDashSound()
    {
        PlaySound(ref DashSFXInstance, DashSFX);
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
        DashSFXInstance.set3DAttributes(attributes);
    }*/

    // Applies a random impulse force to the character
    protected override void Execute()
    {
        ViewmodelScript.viewmodelAnim.SetTrigger("dash");

        if (MovementDirection() == Vector3.zero)
        {
            Vector3 stillForce = gameObject.transform.forward * forceScalar;
            if (player.IsGrounded())
                stillForce = stillForce.normalized * groundSpeed;

            // Zeros out the player's current velocity so dash feels more snappy
            characterRB.velocity = Vector3.zero;
            characterRB.AddForce(stillForce, forceMode);
        }
        else
        {
            Vector3 force = MovementDirection() * forceScalar + upwardVector;
            if (player.IsGrounded())
                force = force.normalized * groundSpeed;

            // Zeros out the player's current velocity so dash feels more snappy
            characterRB.velocity = Vector3.zero;
            characterRB.AddForce(force, forceMode);

            //characterController.SimpleMove(force);
        }

        EmpowerKick();
        player.PlayDashSound();
        Debug.Log("Dashed");

        //StartCoroutine(DelayedSpeedLimit());
        
    }

    IEnumerator DelayedSpeedLimit()
    {
        yield return new WaitForSeconds(speedLimitDelay);
        if (player.IsGrounded())
            characterRB.velocity = characterRB.velocity.normalized * speedLimit;
    }

    protected override void ChildTick()
    {
        if (IsKickEmpowered())
            empoweredKick -= Time.fixedDeltaTime;
    }

    public bool IsKickEmpowered()
    {
        return empoweredKick > 0;
    }

    private void EmpowerKick()
    {
        empoweredKick = empoweredKickDuration;
    }

    public void ResetEmpoweredKick()
    {
        empoweredKick = 0;
    }

    //Delegate Methods
    Vector3 FacingDirection()
    {
        return rotationAnchor.transform.forward;
    }

    Vector3 InputDirection()
    {
        return player.GetMoveInput3D();
    }


}

public enum MovementType
{
    FACING_DIRECTION,
    INPUT_DIRECTION
}

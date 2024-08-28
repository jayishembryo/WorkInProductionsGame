using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleToss : AbstractAbility
{
    [Header("Assignments")]
    [SerializeField]
    GameObject bottlePrefab;

    [SerializeField]
    int objectPoolSize = 3;

    [SerializeField]
    Transform bottleSpawnpoint;

    [SerializeField]
    Transform aim;

    [SerializeField]
    float animDuration = 0.25f;

    [Header("Stats")]
    [SerializeField]
    float throwForce;

    [SerializeField]
    float upwardForce;

    Vector3 upVector;

    Stack<Bottle> inactiveBottles = new();

    private void Start()
    {
        for(int i = 0; i < objectPoolSize; i++)
        {
            GameObject obj = Instantiate(bottlePrefab);

            Bottle bottle = obj.GetComponent<Bottle>();

            bottle.Deactivation += OnBottleDeactivation;

            bottle.Deactivate();
        }

        upVector = new Vector3(0, upwardForce, 0);
    }

    protected override void ChildTick()
    {
        
    }

    protected override void Execute()
    {
        StartCoroutine(DelayedBottleThrow());
        //Debug.Log("Tossed Bottle");
    }

    IEnumerator DelayedBottleThrow()
    {
        yield return new WaitForSeconds(animDuration);

        Bottle bottle = inactiveBottles.Pop();

        bottle.Activate(bottleSpawnpoint.position, aim.forward * throwForce + upVector);

    }

    private void OnBottleDeactivation(object sender, BottleEventArgs e)
    {
        inactiveBottles.Push(e.Bottle);
    }
}

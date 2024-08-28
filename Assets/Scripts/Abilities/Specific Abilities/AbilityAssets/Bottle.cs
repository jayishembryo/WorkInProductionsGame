using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bottle : MonoBehaviour
{
    [SerializeField]
    float knockbackForce;

    [SerializeField]
    PortalDetector portalDetector;

    [SerializeField]
    float upwardForce = 3;

    [SerializeField]
    float distanceMult = 0.5f;

    [SerializeField]
    float duration = 4;

    [SerializeField]
    Vector3 torque;

    [Header("Puddles")]
    [SerializeField]
    GameObject puddlePrefab;

    [SerializeField]
    int puddlePoolAmt = 2;

    Stack<Puddle> inactivePuddles = new();

    Vector3 upVector;

    Rigidbody rb;

    bool george = false;

    float timeTillDeactivation = 100;

    public delegate void DeactivationHandler(object sender, BottleEventArgs e);
    public event DeactivationHandler Deactivation;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        upVector = new Vector3(0, upwardForce, 0);

        for (int i = 0; i < puddlePoolAmt; i++)
        {
            GameObject obj = Instantiate(puddlePrefab);

            Puddle puddle = obj.GetComponent<Puddle>();

            puddle.Deactivation += OnPuddleDeactivation;

            puddle.Deactivate();
        }
    }

    private void FixedUpdate()
    {
        Gravity();
        if (timeTillDeactivation > 0)
            timeTillDeactivation -= Time.fixedDeltaTime;

        else if(george)
            Deactivate();
    }

    void Gravity()
    {
        float gravForce = (rb.velocity.y < 0 ? GravityManager.Instance.BottleGravity * GravityManager.Instance.BottleFallMult : GravityManager.Instance.BottleGravity);

        rb.AddForce(Vector3.down * gravForce);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ground") && !other.CompareTag("Enemy"))
            return;

        Puddle p = inactivePuddles.Pop();

        p.Activate(transform.position);

        if (!other.CompareTag("Enemy"))
        {
            Deactivate();
            return;
        }
        
        Vector3 targetPos = portalDetector.GetTargetPortal();
        if (targetPos == Vector3.zero)
            targetPos = other.transform.position + rb.velocity;
        Vector3 forceToAdd = VectorUtils.DistanceVector(transform.position, targetPos, false);
        float distance = forceToAdd.magnitude;

        other.GetComponent<Rigidbody>().AddForce((forceToAdd.normalized * (distance * distanceMult * knockbackForce)) + upVector);

        Deactivate();
    }

    public void Deactivate()
    {
        Deactivation.Invoke(this, new BottleEventArgs(this));

        george = false;

        gameObject.SetActive(false);
    }

    public void Activate(Vector3 spawnPoint, Vector3 launchVector)
    {
        gameObject.SetActive(true);

        george = true;

        transform.position = spawnPoint;

        timeTillDeactivation = duration;

        StartCoroutine(DelayedLaunch(launchVector));
    }

    IEnumerator DelayedLaunch(Vector3 launchVector)
    {
        yield return new WaitForFixedUpdate();

        rb.velocity = Vector3.zero;

        rb.AddForce(launchVector, ForceMode.Impulse);

        rb.AddRelativeTorque(torque, ForceMode.Impulse);
    }

    private void OnPuddleDeactivation(object sender, PuddleEventArgs e)
    {
        inactivePuddles.Push(e.Puddle);
    }
}

public class BottleEventArgs : EventArgs
{
    public Bottle Bottle { get; private set; }

    public BottleEventArgs(Bottle bottle)
    {
        this.Bottle = bottle;
    }
}

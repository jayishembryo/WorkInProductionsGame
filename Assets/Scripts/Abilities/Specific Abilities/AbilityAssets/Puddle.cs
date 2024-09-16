using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : MonoBehaviour
{
    [SerializeField]
    float slowAmt = 0.4f;

    [SerializeField]
    float duration = 5;

    [SerializeField]
    float globalYHeight = 4;

    List<EnemyBehavior> currentInPuddle = new();

    public delegate void DeactivationHandler(object sender, PuddleEventArgs e);
    public event DeactivationHandler Deactivation;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy"))
            return;

        EnemyBehavior enemy = other.GetComponent<EnemyBehavior>();

        ApplySlow(enemy);

        currentInPuddle.Add(enemy);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Enemy"))
            return;

        EnemyBehavior enemy = other.GetComponent<EnemyBehavior>();

        RemoveSlow(enemy);

        currentInPuddle.Remove(enemy);
    }

    public void ApplySlow(EnemyBehavior enemy)
    {
        enemy.ApplySlow(slowAmt);
    }

    public void RemoveSlow(EnemyBehavior enemy)
    {
        enemy.RemoveSlow();
    }

    public void Activate(Vector3 spawnPoint)
    {
        gameObject.SetActive(true);

        spawnPoint.y = globalYHeight;

        transform.position = spawnPoint;

        StartCoroutine(DelayedDeactivation());
    }

    public void Deactivate()
    {
        Deactivation.Invoke(this, new PuddleEventArgs(this));

        foreach(EnemyBehavior enemy in currentInPuddle)
        {
            enemy.RemoveSlow();
        }

        gameObject.SetActive(false);
    }

    IEnumerator DelayedDeactivation()
    {
        yield return new WaitForSeconds(duration);
        Deactivate();
    }
}

public class PuddleEventArgs : EventArgs
{
    public Puddle Puddle { get; private set; }

    public PuddleEventArgs(Puddle puddle)
    {
        this.Puddle = puddle;
    }
}



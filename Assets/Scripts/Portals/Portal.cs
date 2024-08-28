using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Portal : MonoBehaviour
{
    [SerializeField]
    float size = 1;

    [SerializeField]
    [Range(0,1)]
    float sizeVariance = 0;

    [SerializeField]
    float duration = 10;

    [SerializeField]
    int pointsAwarded = 1;

    [Header("Spawning")]
    [SerializeField]
    float spawnRadius = 50;

    [SerializeField]
    float radiusVariance = 10;

    [SerializeField]
    float height = 10;

    [SerializeField]
    float heightVariance = 1;

    [SerializeField]
    UnityEvent OnEnemyIn;

    public delegate void DeactivationHandler(object sender, PortalEventArgs e);
    public event DeactivationHandler Deactivation;

    [HideInInspector]
    public int Slot = -1;

    private void Start()
    {
        float finalSize = size * (1 + UnityEngine.Random.Range(-sizeVariance, sizeVariance));
        transform.localScale = new Vector3(finalSize, finalSize, finalSize);

        StartCoroutine(DelayedDeactivate());
    }

    private void Update()
    {
        Transform player = PlayerManager.Instance.GetPlayerTransform();

        Quaternion lookRotation = Quaternion.LookRotation((player.position - transform.position).normalized);

        transform.rotation = lookRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy"))
            return;

        // Award Points Here
        //Debug.Log("Points Scored!");

        ScoreboardManager.Instance.AddScore(pointsAwarded);
        PlayerManager.Instance.AddFood(pointsAwarded);

        OnEnemyIn.Invoke();

        Destroy(other.gameObject);
    }

    IEnumerator DelayedDeactivate()
    {
        yield return new WaitForSeconds(duration);
        Deactivate();
    }

    public void Activate()
    {
        gameObject.SetActive(true);

        transform.position = GenerateSpawnPosition();
    }

    public void Deactivate()
    {
        Deactivation.Invoke(this, new PortalEventArgs(this));



        gameObject.SetActive(false);
    }

    private Vector3 GenerateSpawnPosition()
    {
        float radius = spawnRadius + UnityEngine.Random.Range(-radiusVariance, radiusVariance);

        float angle = Slot * Mathf.PI * 2 / PortalManager.Instance.GetPortalAmount();
        float cosine = Mathf.Cos(angle);
        float sine = Mathf.Sin(angle);
        return new Vector3(cosine, (height + UnityEngine.Random.Range(-heightVariance, heightVariance)) / radius, sine) * radius;
    }
}

public class PortalEventArgs : EventArgs
{
    public Portal Portal { get; private set; }

    public PortalEventArgs(Portal portal)
    {
        Portal = portal;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public static PortalManager Instance;

    [SerializeField]
    private List<GameObject> portalPrefabs;
    [SerializeField]
    private float radius = 15f;
    [SerializeField]
    float height = 1;
    [SerializeField]
    float heightVariance = 0.1f;
    [SerializeField]
    float radiusVariance = 4;

    [SerializeField]
    int amount = 10;

    [SerializeField]
    int amtToSpawn = 5;

    [SerializeField]
    float cycleDelay = 12;

    private Vector3 position;

    List<Portal> inactivePortals = new();

    private List<Portal> portals = new();

    private bool stopCycle = false;
    List<int> availableSlots = new();

    private void Start()
    {
        Instance = this;

        for (int i = 0; i < amount; i++)
            availableSlots.Add(i);

        GeneratePortals();

        //SpawnPortal();
        BeginPortalCycle();
    }

    public void StartSpawningPortals()
    {
        for(int i = 0; i < amtToSpawn; i++)
        {
            SpawnPortal();
        }
    }

    private void GeneratePortals()
    {
        foreach(GameObject obj in portalPrefabs)
        {
            for(int i = 0; i < amtToSpawn; i++)
            {
                GameObject temp = Instantiate(obj);

                Portal p = temp.GetComponent<Portal>();

                portals.Add(p);

                p.Deactivation += OnDeactivation;

                p.gameObject.SetActive(false);

                inactivePortals.Add(p);
            }
            
        }
    }

    public void BeginPortalCycle()
    {
        //StartCoroutine(PortalCycler());
        StartSpawningPortals();
    }

    public void StopPortalCycle()
    {
        stopCycle = true;

        /*foreach(Portal p in portals)
        {
            p.gameObject.SetActive(false);
        }*/
    }

    IEnumerator PortalCycler()
    {
        do
        {
            CyclePortals();
            yield return new WaitForSeconds(cycleDelay);
        } while (!stopCycle);
        
    }

    public void SpawnPortals()
    {
        for (int i = 0; i < amount; i++)
        {
            float angle = i * Mathf.PI * 2 / 10;
            float cosine = Mathf.Cos(angle);
            float sine = Mathf.Sin(angle);
            position = new Vector3(cosine, height, sine) * radius;
            portals.Add(Instantiate(GetRandomPrefab(), position, Quaternion.identity).GetComponent<Portal>());
            portals[i].gameObject.SetActive(false);
        }
    }

    public void SpawnPortal()
    {
        Portal p = inactivePortals[Random.Range(0, inactivePortals.Count)];
        inactivePortals.Remove(p);

        int slot = availableSlots[Random.Range(0, availableSlots.Count)];
        availableSlots.Remove(slot);

        p.Slot = slot;

        p.Activate();
    }

    private GameObject GetRandomPrefab()
    {
        return portalPrefabs[Random.Range(0, portalPrefabs.Count)];
    }

    private void CyclePortals()
    {
        Vector3[] spawnPositions = GenerateSpawnPositions();
        for (int i = 0; i < amount; i++)
        {
            portals[i].transform.position = spawnPositions[i];
            portals[i].gameObject.SetActive(true);
        }
    }

    Vector3[] GenerateSpawnPositions()
    {
        Vector3[] spawnPositions = new Vector3[amount];

        for (int i = 0; i < amount; i++)
        {
            float radius = this.radius + Random.Range(-radiusVariance, radiusVariance);

            float angle = i * Mathf.PI * 2 / amount;
            float cosine = Mathf.Cos(angle);
            float sine = Mathf.Sin(angle);
            spawnPositions[i] = new Vector3(cosine, (height + Random.Range(-heightVariance, heightVariance)) / radius, sine) * radius;
        }

        return spawnPositions;
    }

    public List<Transform> GetPortalTransforms()
    {
        List<Transform> returnList = new();
        foreach (Portal p in portals)
            returnList.Add(p.transform);

        return returnList;
    }

    public int GetPortalAmount()
    {
        return amount;
    }

    private void OnDeactivation(object sender, PortalEventArgs e)
    {
        inactivePortals.Add(e.Portal);
        availableSlots.Add(e.Portal.Slot);

        if(!stopCycle)
            SpawnPortal();
    }
}

public enum PortalType
{
    STANDARD,
    HEALING,
    MOVING
}

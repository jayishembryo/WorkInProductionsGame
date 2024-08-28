using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public EnemyBehavior EB;

    [SerializeField]
    private float respawnCooldown = 30f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NewWave());
    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator NewWave()
    {
        EB.SpawnEnemy();
        yield return new WaitForSecondsRealtime(respawnCooldown);
        if (respawnCooldown > 5f)
        {
            respawnCooldown -= 1f;
        }
        Debug.Log(respawnCooldown);
        StartCoroutine(NewWave());
    }
}

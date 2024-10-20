using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBehaviour : MonoBehaviour
{
    public int health;
    [SerializeField] private int currentAttack;
    private Animator anim;
    public Transform[] attackSpawnPoints; // 0: tip of lightning ray, 1: side of boss,
    public GameObject[] attackObjects; // 0: small lightning area, 1: large lightning area, 2: meteor
    public SpawnManagerForBoss spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        spawnManager = FindObjectOfType<SpawnManagerForBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetInteger("health", health);
    }

    public void StartNextAttackAtVariableTime()
    {
        Invoke(nameof(PerformAttack), Random.Range(0.7f, 1.95f));
    }

    public void PerformAttack()
    {
        int chosenAttack = Random.Range(1, 3);
        switch (chosenAttack)
        {
            case 1:
                anim.SetTrigger("fire");
                break;
            case 2:
                anim.SetTrigger("lightning");
                break;
            case 3:
                anim.SetTrigger("idk");
                break;
        }
    }
    public void PerformSpecialAttack(int whichOne)
    {
        switch (whichOne)
        {
            case 0:
                anim.SetTrigger("beginFlood");
                break;
            case 1:
                anim.SetTrigger("ice");
                break;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyKnocked"))
        {
            TakeDamage();
        }
    }
    public void TakeDamage()
    {
        anim.SetTrigger("hurt");
        health -= 1;
        Debug.Log("Boss has been damaged!");
    }
    public void SummonLighntingArea(int size)
    {
        switch (size)
        {
            case 0:
                Instantiate(attackObjects[0], attackSpawnPoints[0].position, Quaternion.identity);
                break;
            case 1:
                Instantiate(attackObjects[1], new Vector3(attackSpawnPoints[1].position.x, attackSpawnPoints[1].position.y, (attackSpawnPoints[1].position.z - 27)), Quaternion.identity);
                break;
            default:
                Debug.Log("Error! Size of Lightning Area outside of knowable range!");
                break;
        }
    }
    public void SummonMeteor()
    {     
        for (int i = 0; i < spawnManager.spawnPoints.Length; i++)
        {
            Instantiate(attackObjects[2], new Vector3((spawnManager.spawnPoints[i].position.x + Random.Range(-4f, 4f)), 115f, (spawnManager.spawnPoints[i].position.z + Random.Range(-4f, 4f))), Quaternion.identity);
        }
    }
    public void EnterFloodedState()
    {
        anim.SetBool("flooded", true);
        Debug.Log("The arena is now flooded!");
    }

    public void DeathFunctions(int progress)
    {
        switch (progress)
        {
            case 0:
                Destroy(spawnManager.gameObject);
                GameObject.Find("EndScreen").GetComponent<Animation>().Play();
                break;
            case 1:
                SceneManager.LoadScene(0);
                break;
        }
    }
}


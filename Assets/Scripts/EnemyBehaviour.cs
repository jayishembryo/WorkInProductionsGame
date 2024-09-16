//Joey Consoli
//Created 1/22/2024
//Changing this format later im lazy xd
//This script tells an enemy to look at the player and move towards them at a set speed

//Jake Gorski
//Modifying 9/3/2024
//Gunna mess up some code and see what happens :)
using System.Collections;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    public float health;
    [SerializeField]
    private float speed = 2f;
    private Rigidbody enemyRB;
    
    [SerializeField]
    private LayerMask whatIsGround, whatIsPlayer;

    //Patrolling
    public UnityEngine.Vector3 walkPoint;
    bool walkPointSet;
    [SerializeField]
    private float walkPointRange;

    //Attacking
    private float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    [SerializeField]
    private float sightRange, attackRange;
    private bool playerInSightRange, playerInAttackRange;

    [SerializeField] private bool knocked;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");  //Finds the player
        agent = GetComponent<NavMeshAgent>();
        enemyRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);


        if (agent.isActiveAndEnabled && knocked == false)
        {
            if (!playerInSightRange && !playerInAttackRange) Patrolling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();
        }
        if (knocked == true)
        {
            agent.enabled = false;
            //transform.Translate(transform.position.x, transform.position.y + 5, transform.position.z);
        }
    }

    private void Patrolling()
    {
        if(!walkPointSet) SearchWalkPoint();

        if(walkPointSet && agent.isActiveAndEnabled == true)
        {   
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude < 2f)
        {
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange,walkPointRange);
        float randomX = Random.Range(-walkPointRange,walkPointRange);

        if (knocked == false) { walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ); }

        if(Physics.Raycast(walkPoint, -transform.up, Mathf.Infinity, whatIsGround))
        {
            walkPointSet = true;
        }

    }

    private void ChasePlayer()
    {
        Vector3 playerVariance = player.transform.position;
        playerVariance.x += Random.Range(0,1.5f);//adding in some difference between each enemy so they don't clump up as much
        playerVariance.z += Random.Range(0,1.5f);
        if (knocked == false) { agent.SetDestination(playerVariance); } //the switch to chasing the player
    }

    private void AttackPlayer()//this is for a claw attack for when the player is up in their face or the other way around.
    {
        //agent.SetDestination(transform.position); //make the enemy stop moving when attacking.

        transform.LookAt(player.transform.position);
        
        if(!alreadyAttacked)
        {   
            //the attack code would go right here if I felt like it rn. Jake - 9/3/2024
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)//enemy gets hit
    {
        
        health -= damage;

        if(health <= 0)
        {
            Invoke(nameof(DestroyEnemy), 2f);
        }
    }

    private void DestroyEnemy()//for adding flavor to a death
    {
        Destroy(gameObject);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    void OnCollisionEnter(Collision other)
    {
        //if(other.gameObject.CompareTag("Ocean"))//if they fall in the water
        {
            //Uncomment this when we have a SplashEffect
            //Instantiate(SplashEffect, transform.position, Quaternion.identity);
        }

        if(other.gameObject.CompareTag("Ground"))  
        {
            agent.enabled = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            if (other.gameObject.layer == 8)//kick layer for if they get kicked by the player
            {
                agent.enabled = false;
                Vector3 launchDirection = player.transform.position - enemyRB.position;
                Vector3 launchDirNormalized = launchDirection.normalized;
                float launchVelocity = player.GetComponent<Rigidbody>().velocity.magnitude;
                enemyRB.AddForce((launchDirNormalized * launchVelocity) + new Vector3(0, 10, 0));
                StartCoroutine(EnemyKnocked());
                Debug.Log(gameObject.name + " has been kicked.");
            }
        }
        if(other.gameObject.layer == 16)
        {

            Debug.Log("gbaeruihfgijnvrskdf");
            DestroyEnemy();
            FindObjectOfType<SpawnManager>().GetComponent<SpawnManager>().enemyHasDied(other.gameObject);

        }
    }
    private IEnumerator EnemyKnocked()
    {
        Debug.Log("AAHHHH");
        knocked = true;
        yield return new WaitForSeconds(2f); //replace this with a scalable variable
        knocked = false;
        agent.enabled = true;
    }
}

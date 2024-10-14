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
    public float damageToPlayer;
    
    [SerializeField]
    private LayerMask whatIsGround, whatIsPlayer;

    //Patrolling
    public UnityEngine.Vector3 walkPoint;
    bool walkPointSet;
    [SerializeField]
    private float walkPointRange;

    //Attacking
    [SerializeField] private float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    [SerializeField]
    private float sightRange, attackRange;
    private bool playerInSightRange, playerInAttackRange;

    [SerializeField] private bool knocked;

    public int enemyID; // used to define what type of enemy this is. 0: normal 1: stinger 2: tank 3: boss?

    public GameObject knockedObject; // object to be spawned in the event of this enemy being kicked
    private ParticleSystem skid; // for use with tank enemy being knocked back

    private Animator anim;

    [SerializeField] public bool DoesHeal;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");  //Finds the player
        agent = GetComponent<NavMeshAgent>();
        enemyRB = GetComponent<Rigidbody>();
        if (enemyID == 2) // if the enemy is a tank
        {
            skid = GetComponent<ParticleSystem>();
        }
        anim = GetComponentInChildren<Animator>();

        agent.speed = speed;

        int chanceToHeal = Random.Range(0, 10);
        if (chanceToHeal < 9)
        {

            DoesHeal = false;

        }
        else if (chanceToHeal == 9)
        {

            DoesHeal = true;
            GetComponentInChildren<SpriteRenderer>().color = Color.green;

        }
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

        anim.SetFloat("speed", enemyRB.velocity.magnitude);
        //Debug.Log(enemyRB.velocity.magnitude);
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
        playerVariance.x += Random.Range(0,3.5f);//adding in some difference between each enemy so they don't clump up as much
        playerVariance.z += Random.Range(0,3.5f);
        if (knocked == false) { agent.SetDestination(playerVariance); } //the switch to chasing the player
    }

    private void AttackPlayer()//this is for a claw attack for when the player is up in their face or the other way around.
    {
        switch (enemyID)
        {
            default:
            case 0:
            case 2:
                agent.SetDestination(transform.position);
                break;
            case 1:
                agent.SetDestination(player.transform.position);
                //agent.SetDestination(player.transform.position);
                break;
        }
        
        if(!alreadyAttacked && agent.enabled)
        {
            anim.SetTrigger("attack");
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
        if (other.gameObject.layer == 8)//kick layer for if they get kicked by the player
        {
            //defines the different reactions to being kicked.
            switch (enemyID)
            {
                case 0:
                case 1:
                    EnemyKnocked();
                    break;
                case 2:
                    if (GetComponentInChildren<TeeterCheckerScript>().isTeetering == false)
                    {
                        anim.SetTrigger("pushed");
                        agent.enabled = false;
                        Vector3 launchDirection = player.transform.position - enemyRB.position;
                        Vector3 launchDirNormalized = launchDirection.normalized;
                        float launchVelocity = player.GetComponent<Rigidbody>().velocity.magnitude;
                        enemyRB.AddForce((launchDirNormalized * launchVelocity));
                        skid.Play(); // this will throw an error if called when the enemy is not a tank
                        Invoke("ReEnableTank", 1.8f);
                        StopCoroutine(nameof(TeeterTank));
                    }
                    else
                    {
                        EnemyKnocked();
                    }
                    break;
                case 3:
                    // poo
                    break;
                default:
                    break;
            }
        }
        if (other.gameObject.layer == 16)
        {

            Debug.Log("Enemy fell like a dumb idiot. -20 aura.");
            DestroyEnemy();
        }
    }

    public IEnumerator TeeterTank()
    {
        Debug.Log(gameObject.name + " object is now in teeter!");
        anim.SetBool("teeter", true);
        agent.speed = 0;
        yield return new WaitForSeconds(4f);
        Debug.Log(gameObject.name + " has fallen out of teeter.");
        anim.SetBool("teeter", false);
        GetComponentInChildren<TeeterCheckerScript>().isTeetering = false;
        anim.SetTrigger("return");
        agent.speed = speed;
        yield return new WaitForSeconds(1f);
        GetComponentInChildren<TeeterCheckerScript>().toldParentToTeeter = false;
        Debug.Log(gameObject.name + " can now be teetered again.");
    }
    private void ReEnableTank()
    {
        agent.enabled = true;
        skid.Stop();
    }
    private void EnemyKnocked()
    {
        Debug.Log(gameObject.name + " has been kicked.");
        if (knockedObject != null)
        {
            Instantiate(knockedObject, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}

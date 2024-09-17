using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockedBehaviour : MonoBehaviour
{
    public Transform player;
    public float projSpeed;
    public Rigidbody rb;
    [SerializeField] private int lifetime = 0;
    public GameObject[] burst; /// Bursts are gameObjects that are spawned when an enemyKnockedObject hits a surface. 0: burstWall 1: burstWater 2: burstSmack 3: burstEnemy
    public GameObject enemyToSpawn;

    /// <summary>
    /// This awake function is used to set the bullet to face its target and move in a forward direction when it is spawned.
    /// </summary>
    private void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>(); //fallback
        }
        player = FindObjectOfType<PlayerController>().transform;
        Vector3 direction = transform.position - player.position;
        transform.rotation = Quaternion.LookRotation(direction);
        rb.velocity = Camera.main.gameObject.transform.forward * projSpeed;
    }

    private void Update()
    {
        transform.LookAt(player.position);

        lifetime++;
        if (lifetime > 10)
        {
        }
        if (lifetime >= 500)
        {
            Debug.Log("Knocked Enemy " + gameObject.name + " deleted of old age");
            KillEnemy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("KillBarrier"))
        {
            Instantiate(burst[0], transform.position, Quaternion.identity);
            KillEnemy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            StartCoroutine(SmackAgainstWall());
            Debug.Log("HI!!!");
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (lifetime > 40)
            {
                Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Knocked enemy has collided with: " + collision.gameObject.name);
            Instantiate(burst[3], transform.position, Quaternion.identity);
            KillEnemy(collision.gameObject);
            KillEnemy(gameObject);
        }
    }

    public void KillEnemy(GameObject killed)
    {
        // report the death of this enemy to the spawn manager here
        Destroy(killed); // destroys the inputed gameObject parameter
    }

    private IEnumerator SmackAgainstWall()
    {
        //fix up the collision on this later so the out of bounds walls are also the colliders that destroy the knockedObjects
        Instantiate(burst[2], transform.position, Quaternion.identity);
        Vector3 savedVelocity = rb.velocity;
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        yield return new WaitForSeconds(0.21f);
        Debug.Log("BYE!!!");
        rb.velocity = savedVelocity;
        rb.useGravity = true;
    }
}
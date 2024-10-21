using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyKnockedBehaviour : MonoBehaviour
{
    public Transform player;
    public float projSpeed;
    public Rigidbody rb;
    [SerializeField] private int lifetime = 0;
    public GameObject[] burst; /// Bursts are gameObjects that are spawned when an enemyKnockedObject hits a surface. 0: burstWall 1: burstWater 2: burstSmack 3: burstEnemy
    public GameObject enemyToSpawn;
    public EnemyBehaviour EnemyBehaviorInstance;

    public ParticleSystem HealthParticles;
    public ParticleSystem.Particle[] GettingParticles;

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

        HealthParticles = burst[4].GetComponent<ParticleSystem>();
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
            Debug.Log("knocked enemy " + gameObject.name + " deleted of old age");

            if (EnemyBehaviorInstance.DoesHeal == true)
            {

                //LOOK INTO PARTICLES
                float healed = Random.Range(5f, 11f);
                HealthSystem.instance.Heal(healed);

                //MAKE THE PARTICLE EFFECT
                //ADD PARTICLE EFFECT TO LIST
                Instantiate(burst[4], transform.position, Quaternion.identity);

                GettingParticles = new ParticleSystem.Particle[HealthParticles.particleCount];
                HealthParticles.GetParticles(GettingParticles);

                for(int i = 0; i < GettingParticles.GetUpperBound(0); i++)
                {

                    float FlyingSpeed = (GettingParticles[i].startLifetime - GettingParticles[i].remainingLifetime) * (10 * Vector3.Distance(player.position, GettingParticles[i].position));
                    GettingParticles[i].velocity = (player.position - GettingParticles[i].position).normalized * FlyingSpeed;
                    GettingParticles[i].position = Vector3.Lerp(GettingParticles[i].position, player.position, Time.deltaTime / 2f);


                }

                HealthParticles.SetParticles(GettingParticles, GettingParticles.Length);

            }

            KillEnemy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("KillBarrier"))
        {
            if (EnemyBehaviorInstance.DoesHeal == true)
            {

                //LOOK INTO PARTICLES
                float healed = Random.Range(10f, 16f);
                HealthSystem.instance.Heal(healed);

                //MAKE THE PARTICLE EFFECT
                //ADD PARTICLE EFFECT TO LIST
                Instantiate(burst[4], transform.position, Quaternion.identity);

                GettingParticles = new ParticleSystem.Particle[HealthParticles.particleCount];
                HealthParticles.GetParticles(GettingParticles);

                for (int i = 0; i < GettingParticles.GetUpperBound(0); i++)
                {

                    float FlyingSpeed = (GettingParticles[i].startLifetime - GettingParticles[i].remainingLifetime) * (10 * Vector3.Distance(player.position, GettingParticles[i].position));
                    GettingParticles[i].velocity = (player.position - GettingParticles[i].position).normalized * FlyingSpeed;
                    GettingParticles[i].position = Vector3.Lerp(GettingParticles[i].position, player.position, Time.deltaTime);


                }

                HealthParticles.SetParticles(GettingParticles, GettingParticles.Length);

            }
            else
            {

                Instantiate(burst[0], transform.position, Quaternion.identity);

            }

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
            if (EnemyBehaviorInstance.DoesHeal == true)
            {

                //LOOK INTO PARTICLES
                float healed = Random.Range(5f, 11f);
                HealthSystem.instance.Heal(healed);

                //MAKE THE PARTICLE EFFECT
                //ADD PARTICLE EFFECT TO LIST
                Instantiate(burst[4], transform.position, Quaternion.identity);

                GettingParticles = new ParticleSystem.Particle[HealthParticles.particleCount];
                HealthParticles.GetParticles(GettingParticles);

                for (int i = 0; i < GettingParticles.GetUpperBound(0); i++)
                {

                    float FlyingSpeed = (GettingParticles[i].startLifetime - GettingParticles[i].remainingLifetime) * (10 * Vector3.Distance(player.position, GettingParticles[i].position));
                    GettingParticles[i].velocity = (player.position - GettingParticles[i].position).normalized * FlyingSpeed;
                    GettingParticles[i].position = Vector3.Lerp(GettingParticles[i].position, player.position, Time.deltaTime);


                }

                HealthParticles.SetParticles(GettingParticles, GettingParticles.Length);

            }
            else
            {

                Instantiate(burst[3], transform.position, Quaternion.identity);

            }
            KillEnemy(collision.gameObject);
            KillEnemy(gameObject);
        }
        if (collision.gameObject.CompareTag("Boss"))
        {
            Debug.Log("Knocked enemy has collided with: " + collision.gameObject.name);
            if (EnemyBehaviorInstance.DoesHeal == true)
            {

                //LOOK INTO PARTICLES
                float healed = Random.Range(5f, 11f);
                HealthSystem.instance.Heal(healed);

                //MAKE THE PARTICLE EFFECT
                //ADD PARTICLE EFFECT TO LIST
                Instantiate(burst[4], transform.position, Quaternion.identity);

                GettingParticles = new ParticleSystem.Particle[HealthParticles.particleCount];
                HealthParticles.GetParticles(GettingParticles);

                for (int i = 0; i < GettingParticles.GetUpperBound(0); i++)
                {

                    float FlyingSpeed = (GettingParticles[i].startLifetime - GettingParticles[i].remainingLifetime) * (10 * Vector3.Distance(player.position, GettingParticles[i].position));
                    GettingParticles[i].velocity = (player.position - GettingParticles[i].position).normalized * FlyingSpeed;
                    GettingParticles[i].position = Vector3.Lerp(GettingParticles[i].position, player.position, Time.deltaTime / 2f);


                }

                HealthParticles.SetParticles(GettingParticles, GettingParticles.Length);

            }
            else
            {

                Instantiate(burst[5], transform.position, Quaternion.identity);

            }
            KillEnemy(gameObject);
        }
    }

    public void KillEnemy(GameObject killed)
    {
        SpawnManager spawnManager = FindObjectOfType<SpawnManager>();

        if (spawnManager != null)
        {
            spawnManager.TotalEnemies = GameObject.FindObjectsOfType<EnemyBehaviour>().Length;
            spawnManager.TotalEnemies -= 1;

            if (spawnManager.TotalEnemies <= 0 && spawnManager.Waiting == false)
            {

                spawnManager.Waiting = true;
                spawnManager.newWaveStart();

            }
        }

        Destroy(killed);

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
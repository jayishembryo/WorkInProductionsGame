using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    int amount = 10;
    [SerializeField]
    float radiusVariance = 4;
    [SerializeField]
    private float radius = 15f;
    [SerializeField]
    private int randomIndex;
    [SerializeField]
    private Vector3 where;


    private Vector3 position; 
    private GameObject player;
    private float speed = 2f;

    float slowedSpeed = 1;
    bool isSlowed = false;

    Rigidbody rb;

    List<Vector3> positions = new List<Vector3>();

    public GameObject blastoffStar;

    private Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");  //Finds the player
        anim = GetComponent<Animator>();
       
    }

    // Find the angle needed for the amount of enemies (this will be translated into the size of the island).
    // Use that angle to find cosine and sine.
    // Use that sine and cosine to create a vector 3 and multiply that by the radius.
    // Instantiate the enemy using that position.
    public void SpawnEnemy()
    {
        // Adding new Vector3s to the positions list.
        positions.Add(new Vector3(30.82f, 16.12f, -10.96f));
        positions.Add(new Vector3(26.89f, 16.12f, -16.26f));
        positions.Add(new Vector3(22.88f, 16.12f, -19.91f));
        positions.Add(new Vector3(31.1653f, 15.81718f, -4.850232f));
        positions.Add(new Vector3(-1.7f, 15.81718f, -22.7f));
        positions.Add(new Vector3(9f, 15.81718f, -22.7f));
        positions.Add(new Vector3(31.1653f, 15.81718f, -4.850232f));
        positions.Add(new Vector3(27.1f, 15.81718f, 9.39f));
        positions.Add(new Vector3(28.59f, 15.81718f, 2.82f));
        positions.Add(new Vector3(21.41f, 15.81718f, 11.6f));
        positions.Add(new Vector3(13.18f, 15.81718f, 16.31f));
        positions.Add(new Vector3(4.6f, 15.81718f, 23.6f));
        positions.Add(new Vector3(-7.6f, 15.81718f, 23.6f));
        positions.Add(new Vector3(-19.5f, 15.81718f, 15.8f));
        positions.Add(new Vector3(-21.6f, 15.81718f, 7f));
        positions.Add(new Vector3(-20.7f, 15.81718f, -3.2f));
        positions.Add(new Vector3(-20.7f, 15.81718f, -15.7f));
        positions.Add(new Vector3(-14.1f, 15.81718f, -22.7f));

        // Spawns 15 enemies. Pick a random spot withing the list and use that number to pick a random Vector3 from the positions list.
        for(int i = 0; i < 15; i++)
        {
            randomIndex = Random.Range(0, (positions.Count));
            where = positions[randomIndex];
            positions.Remove(where);
            Instantiate(enemy, where, Quaternion.identity);
        }
    }

    private void FixedUpdate()
    {
        Gravity();
    }

    private void Gravity()
    {
        float gravForce = (rb.velocity.y < 0 ? GravityManager.Instance.EnemyGravity * GravityManager.Instance.EnemyFallMult : GravityManager.Instance.EnemyGravity);
        rb.AddForce(Vector3.down * gravForce);
        //Debug.Log(gravForce);
    }

    private void Update()
    {
        Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(targetPosition);  //Makes the front of the object look towards the player, good for a 2d sprite in 3d space

        float currentSpeed = isSlowed ? slowedSpeed : speed;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, currentSpeed * Time.deltaTime);
        //Moves the enemy towards the player from the previous position by a set amount every frame

        //Updates the enemy animations if the enemy is moving or not
        if (gameObject.GetComponent<Rigidbody>().velocity.x != 0 || gameObject.GetComponent<Rigidbody>().velocity.z != 0)
        {
           // anim.SetBool("Moving", true);
        }
        else
        {
            //anim.SetBool("Moving", false);
        }
    }

    public void ApplySlow(float slowAmt)
    {
        isSlowed = true;
        slowedSpeed = speed * (1 - slowAmt);
    }

    public void RemoveSlow()
    {
        isSlowed = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("KillBarrier"))
        {
            Instantiate(blastoffStar, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    //public Vector3[] GenerateSpawnPositions()
    //{
    //    Vector3[] spawnPositions = new Vector3[amount];

    //    for (int i = 0; i < amount; i++)
    //    {
    //        float radius = this.radius + Random.Range(-radiusVariance, radiusVariance);

    //        float angle = i * Mathf.PI * 2 / 10;
    //        float cosine = Mathf.Cos(angle);
    //        float sine = Mathf.Sin(angle);
    //        spawnPositions[i] = new Vector3(cosine, Random.Range(-radiusVariance, radiusVariance), sine) * radius;
    //    }

    //    return spawnPositions;
    //}
}

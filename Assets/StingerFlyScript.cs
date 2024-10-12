
// Used with Stinger enemy to control fly height and timing
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StingerFlyScript : MonoBehaviour
{
    public float flyOffset;
    private EnemyBehaviour stingerBehaviour;
    private NavMeshAgent agent;
    public GameObject swoopDestination;

    // Start is called before the first frame update
    void Start()
    {
        stingerBehaviour = GetComponentInParent<EnemyBehaviour>();
        agent = GetComponentInParent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.baseOffset = flyOffset; // This controls the flying height, changed via animation keyframes
    }

    /// <summary>
    /// called via animation event, sets new speed and destination so the swoop attack can work the best it can
    /// </summary>
    public void SwoopAction(int progress)
    {
        switch (progress)
        {
            case 0:
                agent.speed += 2;
                agent.SetDestination(swoopDestination.transform.position);
                break;
            case 1:
                agent.speed -= 4;
                break;
            case 2:
                agent.speed += 2;
                agent.SetDestination(GameObject.Find("Player 1").transform.position);
                break;
            default:
                Debug.Log("Error! 'Progress' parameter not correctly set for function 'SwoopAction'!");
                break;
        }
    }
}

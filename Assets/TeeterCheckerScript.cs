
// Used with the tank enemy to check if it is teetering over an edge
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeeterCheckerScript : MonoBehaviour
{
    private EnemyBehaviour tankBehaviour;
    RaycastHit hit;
    public bool isTeetering;
    public bool toldParentToTeeter;
    Vector3 rayDirection;

    // Start is called before the first frame update
    void Start()
    {
        tankBehaviour = GetComponentInParent<EnemyBehaviour>();
        toldParentToTeeter = false;
    }

    // Update is called once per frame
    void Update()
    {
        //rayDirection = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);

        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);
            Debug.Log(hit.collider.tag);
            if (hit.collider.CompareTag("Ground"))
            {
                Debug.Log(gameObject.name + " object is not teetering");
            }
            else
            {
                if (toldParentToTeeter == false)
                {
                    StartCoroutine(tankBehaviour.TeeterTank());
                    isTeetering = true;
                    toldParentToTeeter = true;
                }
                Debug.Log(gameObject.name + " object is teetering!!");
            }
        }
    }
}

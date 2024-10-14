using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtSingleAxis : MonoBehaviour
{
    public GameObject target;
    [SerializeField] private bool lookAtCam;

    void Start()
    {
        if (lookAtCam == true)
        {
            target = Camera.main.gameObject;
        }
    }
    void Update()
    {
        Vector3 targetPosition = new Vector3(target.transform.position.x,
        transform.position.y, target.transform.position.z);
        transform.LookAt(targetPosition);
    }
}
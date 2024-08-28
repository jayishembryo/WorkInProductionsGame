using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalDetector : MonoBehaviour
{
    [SerializeField]
    Rigidbody parentRB;

    [SerializeField]
    float maxRange = 150;

    readonly List<Transform> storedPortals = new();

    private void Start()
    {
        parentRB.GetComponent<Bottle>().Deactivation += OnDeactivation;
    }

    private void FixedUpdate()
    {
        AlignWithVelocity();
    }

    void AlignWithVelocity()
    {
        transform.rotation = Quaternion.LookRotation(parentRB.velocity);
    }

    public Vector3 GetTargetPortal()
    {
        //return VectorUtils.GetClosestPointOfLine(transform.position, parentRB.velocity.normalized * maxRange + transform.position, VectorUtils.ConvertTransformsToPositions(storedPortals));
        return VectorUtils.GetClosestPointOfLine(transform.position, parentRB.velocity.normalized * maxRange + transform.position, VectorUtils.ConvertTransformsToPositions(PortalManager.Instance.GetPortalTransforms()));
    }

    private void OnTriggerEnter(Collider other)
    {
        storedPortals.Add(other.transform);
        Debug.Log("Portal Enter | Count: " + storedPortals.Count);
    }

    private void OnTriggerStay(Collider other)
    {
        if (storedPortals.Contains(other.transform))
            return;

        storedPortals.Add(other.transform);
        Debug.Log("Portal Enter | Count: " + storedPortals.Count);
    }

    private void OnTriggerExit(Collider other)
    {
        storedPortals.Remove(other.transform);
        Debug.Log("Portal Exit | Count: " + storedPortals.Count);
    }

    private void OnDeactivation(object sender, BottleEventArgs e)
    {
        storedPortals.Clear();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionGizmo : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, VectorUtils.DirectionVector(transform));
    }
}

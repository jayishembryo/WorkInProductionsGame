using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

[ExecuteInEditMode]
public class NavMeshLinkSpline : MonoBehaviour
{
    /*[SerializedField]
    private Spline splineVisualization;
    [SerializedField]
    private navMeshLink navMeshLinkData;
    [SerializedField, Min(0.01f)]
    private float _heightOffset = 1;//used for midpoint of spline
    [SerializedField, Range(0.25f, 0.75f)]
    private float placementOffset = 0.5f;

# if UNITY_EDITOR

    void Update()// runs only in editor for display and not add to complied code
    {
        if(splineVisualization != null && navMeshLinkData != null)
        {
            Vector3 start = transform.TransformPoint(navMeshLinkData.startPoint);
            Vector3 end = transform.TransformPoint(navMeshLinkData.endPoint);
            Vector3 midPointPosition = Vector3.Lerp(start, end, placementOffset);
            midPointPosition.y += heightOffset;
            splineVisualization.SetPoints(start, midPointPosition, end);//passes points to Jumpspline to get calucations and constantly updates in editor
        }
    }

#endif
*/
}

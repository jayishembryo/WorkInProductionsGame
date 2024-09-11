using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSpline : MonoBehaviour
{
    /*[SerializedField]
    private Transform start, middle, end;

    [SerializedField]
    private bool showGizmos = true;

    private Vector3 CalculatePosition(float value, Vector3 startPos, Vector3 endPos, Vector3 midPos)//calculating the positions to create the base of the spline.
    {
        value = Mathf.Clamp01(value);
        Vector3 startMiddle = Vector3.Lerp(startPos, midPos, value);
        Vector3 middleEnd = Vector3.Lerp(midPos, endPos, value);
        return Vector3.Lerp(startMiddle,middleEnd, value);
    }

    public Vector3 CalculatePosition(float interpolationAmount) => CalculatePosition(interpolationAmount, start.position, end.position, middle.position);//calculates position of spline parts

    public Vector3 CalculatePositionCustomStart(float interpolationAmount, Vector3 startPosition) => CalculatePosition(interpolationAmount, startPosition, end.position, middle.position);//first part of the spline calculation

    public Vector3 CalculatePositionCustomEnd(float interpolationAmount, Vector3 endPosition) => CalculatePosition(interpolationAmount, start.position, endPosition, middle.position);//second half of the spline calculation

    public void SetPoints(Vector3 startPoint, Vector3 midPointPosition, Vector3 endPoint)//takes the transforms and assigns them to variables.
    {
        if(start != null && middle != null && end != null)
        {
            start.position = startPoint;
            middle.Position = midPointPosition;
            end.position = endPoint;
        }
    }

    private void OnDrawGizmos()//DRAWS THE SPLINE 
    {
        if(showGizmos && start != null && middle != null && end != null)
        {
            showGizmos.color = Color.red;
            showGizmos.DrawSphere(start.position,0.1f);
            showGizmos.DrawSphere(end.position,0.1f);
            showGizmos.DrawSphere(middle.position,0.1f);
            showGizmos.color = Color.magenta;
            int granularity = 5;

            for(int i = 0; i < granularity; i++)
            {
                Vector3 startPoint = i == 0 ? start.Position : CalculatePosition(i / (float)granularity);
                Vector3 endPoint = i == granularity ? end.Position : CalculatePosition((i + 1) / (float)granularity);
                showGizmos.DrawLine(startPoint,endPoint);
            }
        }
    }*/
}

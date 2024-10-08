using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorUtils
{

    // Get a scaled Vector3 of any transform that points in the direction it is facing
    public static Vector3 DirectionVector(Transform transform, float scalar = 1)
    {
        return (transform.position + transform.forward) * scalar;
    }

    public static Vector3 ClampHorizontalVelocity(Vector3 initialVector, Vector3 newVector, float maxMagnitude)
    {
        initialVector.y = 0;
        newVector.y = 0;
        Vector3 returnVector = initialVector + newVector;

        float magnitudeDifference = maxMagnitude - initialVector.magnitude;
        /*if (returnVector.magnitude > maxMagnitude)
        {
            return Vector3.ClampMagnitude(returnVector, maxMagnitude);
        }*/

        if (magnitudeDifference <= 0)
            return Vector3.zero;


        return returnVector;
    }

    public static Vector3 GetClosestPointOfLine(Vector3 lineStart, Vector3 lineEnd, Vector3 objectPosition)
    {
        Vector3 heading = (lineEnd - lineStart);
        float magnitudeMax = heading.magnitude;
        heading.Normalize();

        Vector3 lhs = objectPosition - lineStart;
        float dotP = Vector3.Dot(lhs, heading);
        dotP = Mathf.Clamp(dotP, 0f, magnitudeMax);
        return lineStart + heading * dotP;
    }

    public static Vector3 GetClosestPointOfLine(Vector3 lineStart, Vector3 lineEnd, List<Vector3> points)
    {
        //Dictionary<Vector3, float> objectDistancesFromLine = new();
        Vector3 closest = Vector3.zero;
        float closestDistance = 99999999;

        foreach(Vector3 p in points)
        {
            Vector3 closestPoint = GetClosestPointOfLine(lineStart, lineEnd, p);

            float distance = (closestPoint - p).magnitude;

            if(distance < closestDistance)
            {
                closest = p;
                closestDistance = distance;
            }

            //objectDistancesFromLine[p] = distance;
        }

        return closest;
    }

    public static Vector3 DistanceVector(Vector3 start, Vector3 end, bool doNormalize)
    {
        Vector3 returnVector = end - start;
        return doNormalize ? returnVector.normalized : returnVector;
    }

    public static List<Vector3> ConvertTransformsToPositions(List<Transform> transforms)
    {
        List<Vector3> returnList = new();

        foreach (Transform t in transforms)
            returnList.Add(t.position);

        return returnList;
    }
}

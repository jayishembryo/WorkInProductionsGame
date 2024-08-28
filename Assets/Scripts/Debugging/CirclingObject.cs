using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclingObject : MonoBehaviour
{
    [SerializeField] private float speed, radius;

    private float angle = 0f;

    private void Update()
    {
        // Update the angle based on time and speed
        angle += speed * Time.deltaTime;

        // Calculate the new position using polar coordinates
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;

        // Set the object's position
        transform.position = new Vector3(x, transform.position.y, z);
    }
}

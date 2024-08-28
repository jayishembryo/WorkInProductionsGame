using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(target, Vector3.up);
    }
    void Update()
    {
        // Rotate the target every frame so it keeps looking at the camera
        transform.LookAt(target);

        // Same as above, but setting the worldUp parameter to Vector3.up
        transform.LookAt(target, Vector3.up);
    }

}

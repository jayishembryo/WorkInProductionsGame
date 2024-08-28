using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXScript : MonoBehaviour
{
    private Transform target;
    [SerializeField]
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Virtual Camera").transform;
        transform.LookAt(target, Vector3.up);
    }
    void Update()
    {
        // Rotate the target every frame so it keeps looking at the camera
        //transform.LookAt(target);

        // Same as above, but setting the worldUp parameter to Vector3.up
        transform.LookAt(target, Vector3.up);
    }

    /// <summary>
    /// Destroys this gameobject. Called thru animation event.
    /// </summary>
    public void KillVFX()
    {
        Destroy(gameObject);
    }
}
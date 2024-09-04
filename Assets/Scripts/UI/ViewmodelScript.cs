// created by Harrison W
// This script holds the variables for the UI animations relating to player actions.
// It also controls the animator and holds the functions that effect player variables during animations.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewmodelScript : MonoBehaviour
{
    public static Animator viewmodelAnim; // static, so it can be called from anywhere without directly referencing this script.
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private Kick kickScript;

    // Start is called before the first frame update
    void Start()
    {

        viewmodelAnim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // The following functions are all implemented as animation events and are called during specific frames of the viewmodel animations.

    public void KickActivate()
    {
        kickScript.hitbox.enabled = true;
        Debug.Log("Kick!");
    }
    public void KickDeactivate()
    {
        kickScript.hitbox.enabled = false;
        kickScript.ApplyForce();
    }

}

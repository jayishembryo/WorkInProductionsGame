using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TideAnimations : MonoBehaviour
{
    Animator boat;

    public void Start()
    {

        boat = GetComponent<Animator>();

    }

    //anims called by animation events
    public void RiseAnim()
    {

        boat.SetTrigger("Rise");

    }

    public void FallAnim()
    {

        boat.SetTrigger("Fall");

    }

}

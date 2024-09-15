using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsefulFunctions : MonoBehaviour
{
    public void SelfDestruct()
    {
        Debug.Log("Destroyed: " + gameObject.name);
        Destroy(gameObject);
    }
}

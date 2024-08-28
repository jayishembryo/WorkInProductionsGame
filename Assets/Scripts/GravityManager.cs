using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{

    public static GravityManager Instance;

    public float EnemyGravity = -10;

    public float EnemyFallMult = 1.75f;

    public float PlayerGravity = -10;

    public float PlayerFallMult = 1.75f;

    public float BottleGravity = -5;

    public float BottleFallMult = -1.25f;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    
}

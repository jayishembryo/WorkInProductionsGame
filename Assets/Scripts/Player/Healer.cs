using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour
{
    [SerializeField]
    float healAmt = 1;

    public void Heal()
    {
        HealthSystem.instance.Heal(healAmt);
    }
}

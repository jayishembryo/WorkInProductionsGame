using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmoothShakePro;

public class UIManager : MonoBehaviour
{
    [Header("Linkage")]
    [SerializeField] private SmoothShakeManager sSM;
    [SerializeField] private SmoothShake sSBoat;
    [SerializeField] private SmoothShake sSBG;

    [Header("Boat Tinkering")]
    [Tooltip("Handles the Boat's rotational and positional " +
        "amplification. Note: Only applies on START!")]
    [SerializeField] private float boatAmp;

    [Header("Background Tinkering")]
    [Tooltip("Handles the Background's rotational and " +
        "positional amplification. Note: Only applies on START!")]
    [SerializeField] private float bgAmp;

    [SerializeField] private bool hasPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        hasPlayed = false;
        PlayMainMenuAnimations();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayMainMenuAnimations()
    {
        // Boat
        sSBoat.SetShakerProperty(0, boatAmp, ShakerProperty.Amplitude);
        sSBoat.SetShakerProperty(1, boatAmp, ShakerProperty.Amplitude);
        sSM.StartShake("Wade");

        // BG
        sSBG.SetShakerProperty(0, bgAmp, ShakerProperty.Amplitude);
        sSBG.SetShakerProperty(1, bgAmp, ShakerProperty.Amplitude);
        sSM.StartShake("Tide");
    }
}

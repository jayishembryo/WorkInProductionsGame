using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // This class is a singleton, we are making sure it is only accessed once, and there are not multiple instances of it.
    private static InputManager _instance;

    [SerializeField]
    float empoweredKickDuration;

    [SerializeField]
    Transform player;

    private float empoweredKick = 0;
    public static InputManager Instance
    {
        get
        {
            return _instance;
        }
    }
    // Creating the playerControls;

    private PlayerControls playerControls;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        playerControls = new PlayerControls();
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void FixedUpdate()
    {
        if (IsKickEmpowered())
        {
            empoweredKick -= Time.fixedDeltaTime;
        }
    }

    public Transform GetPlayerTransform()
    {
        return player;
    }

    // Gets the player movement values
    public Vector2 GetPlayerMovement()
    {
        return playerControls.Player.Movement.ReadValue<Vector2>();
    }

    // Gets the mouse values
    public Vector2 GetMouseDelta()
    {
        return playerControls.Player.Look.ReadValue<Vector2>();
    }

    // Sees if the player has jumped.
    public bool PlayerJumped()
    {
        return playerControls.Player.Jump.triggered;
    }

    // Checks if player dashed
    public bool PlayerDashed()
    {
        return playerControls.Player.Dash.triggered;
    }

    // Checks if player kicked
    public bool PlayerKicked()
    {
        return playerControls.Player.Kick.triggered;
    }

    /*public bool KickPressed()
    {
        return playerControls.Player.Kick.
    }*/

    public bool IsKickEmpowered()
    {
        return empoweredKick > 0;
    }

    public void EmpowerKick()
    {
        empoweredKick = empoweredKickDuration;
    }

    public void ResetEmpoweredKick()
    {
        empoweredKick = 0;
    }

}

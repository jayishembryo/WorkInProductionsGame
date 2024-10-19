using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using FMODUnity;
using FMOD.Studio;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [Header("Physics")]
    [SerializeField]
    public PhysicMaterial physicMaterial;
    [SerializeField]
    private float groundSpeedLimit = 10;
    [SerializeField]
    float airSpeedLimit = 0.4f;
    [SerializeField]
    float airDrag;
    [SerializeField]
    float groundDrag;
    //[SerializeField]
    //private float gravityValue = -9.81f;
    [SerializeField]
    private GameObject enemy;
    
    [Header("Abilities")]
    [SerializeField]
    Kick kickAbility;
    [SerializeField]
    Dash dashAbility;
    [SerializeField]
    Grapple grappleAbility;

    [Header("SoundEffects")]
    [SerializeField]
    private EventReference DashSFX;
    private EventInstance DashSFXInstance;
    [SerializeField]
    private EventReference WalkingSFX;
    private EventInstance WalkingSFXInstance;
    private bool isWalkingPlaying = false;

    [SerializeField]
    float enemySpawnCD = 0.5f;

    float timeTillNextEnemySpawn = 0;

    //private CharacterController controller;
    //private Vector3 playerVelocity;
    //private bool groundedPlayer;
    //private InputManager inputManager;
    [SerializeField]
    private Transform cameraTransform;
    public EnemyBehavior EB;

    public PlayerInput PlayerInputInstance;
    public InputAction Movement;
    public InputAction Pause;
    public InputAction Grappling;
    Grapple grapplingInstance;
    public bool isMoving;
    public float moveDirection;
    public Vector2 moveInput;
    private Rigidbody rb;

    public bool IsTouchingGround = false;

    [SerializeField]
    public GameObject pauseMenu;
    [SerializeField]
    private ScoreboardManager ScoreboardManager;

    float whenToAddTime = 1;
    float lastAddedToTime = 0;

    [SerializeField]
    private Animator playerAnim;

    public GameObject FireScreen;
    public GameObject BlazeEffect;

    [Header("Slope Handling")]
    [SerializeField]
    private float maxSlopeAngle;
    private RaycastHit slopeHit;
    [SerializeField]
    private float playerHeight;

    [SerializeField] public GameObject HealthScreen;

    // Creates the controller, gets an instance of the InputManager, and the camera transform.
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        PlayerInputInstance = GetComponent<PlayerInput>();
        PlayerInputInstance.currentActionMap.Enable();

        Movement = PlayerInputInstance.currentActionMap.FindAction("Movement");
        Pause = PlayerInputInstance.currentActionMap.FindAction("Pause");
        Grappling = PlayerInputInstance.currentActionMap.FindAction("Grapple");

        Movement.started += Movement_started;
        Movement.canceled += Movement_canceled;
        Pause.started += Pause_started;
        Pause.canceled += Pause_canceled;

        Grappling.started += Grappling_started;
        Grappling.canceled += Grappling_canceled;
        //controller = GetComponent<CharacterController>();
        //inputManager = InputManager.Instance;
        //cameraTransform = Camera.main.transform;
        

        Cursor.lockState = CursorLockMode.Locked;

        rb.drag = airDrag;
        Time.timeScale = 1.0f;

        grapplingInstance = GameObject.FindObjectOfType<Grapple>();

        Physics.IgnoreLayerCollision(7, 16);

    }

    private void Pause_canceled(InputAction.CallbackContext obj)
    {
        //print("Pause cancelled");
    }

    private void Pause_started(InputAction.CallbackContext obj)
    {
        if(ScoreboardManager.GameIsRunning == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0.0f;
            pauseMenu.SetActive(true);
            //Debug.Log("Pausing");
        }
    }

    private void Movement_canceled(InputAction.CallbackContext obj)
    {
        //print("Player shouldn't be moving");
        isMoving = false;
    }

    private void Movement_started(InputAction.CallbackContext obj)
    {
        //print("Player should be moving");
        isMoving = true;
    }

    private void Grappling_started(InputAction.CallbackContext obj)
    {
        grapplingInstance.StartGrapple();
        playerAnim.SetBool("point", true);
    }

    private void Grappling_canceled(InputAction.CallbackContext obj)
    {
        grapplingInstance.StopGrapple();
        playerAnim.SetBool("point", false);
    }

    private void FixedUpdate()
    {
        Look();
        Move();
        Gravity();

        lastAddedToTime += Time.fixedDeltaTime;

        if (timeTillNextEnemySpawn > 0)
            timeTillNextEnemySpawn -= Time.fixedDeltaTime;
        //Debug.Log("Drag: " + rb.drag);
        //Debug.Log("Velocity: " + rb.velocity.magnitude);
    }

    private void Gravity()
    {
        float gravForce = (rb.velocity.y < 0 && !IsTouchingGround ? GravityManager.Instance.PlayerGravity * GravityManager.Instance.PlayerFallMult : GravityManager.Instance.PlayerGravity);

        rb.AddForce(Vector3.down * gravForce);

        //Debug.Log(gravForce);
    }

    private void Move()
    {
        Vector3 newVelocity = GetMoveInput3D();
        newVelocity = playerSpeed * newVelocity.normalized;

        newVelocity = VectorUtils.ClampHorizontalVelocity(rb.velocity, newVelocity, (IsTouchingGround ? groundSpeedLimit : airSpeedLimit));

        rb.AddForce(newVelocity, ForceMode.Acceleration);
        //rb.AddRelativeForce(1000 * Time.fixedDeltaTime * newVelocity, ForceMode.Acceleration);
        /*
        if(OnSlope())
        {
            rb.AddForce(GetSlopeMoveDirection() * playerSpeed * 1.1f, ForceMode.Force);
        }
        */
        //turns off gravity while on slope to prevent sliding.
        rb.useGravity = !OnSlope();

    }

    public Vector2 GetMoveInput()
    {
        return Movement.ReadValue<Vector2>().normalized;
    }

    public Vector3 GetMoveInput3D()
    {
        moveInput = GetMoveInput();

        return moveInput.x * transform.right + moveInput.y * transform.forward;
    }

    private void Look()
    {
        transform.rotation = Quaternion.Euler(0, cameraTransform.rotation.eulerAngles.y, 0);
    }

    public void Jump()
    {
        if (!IsTouchingGround)
            return;

        rb.velocity += new Vector3(0, jumpHeight, 0);
    }

    public void SetTouchedGround(bool touchedGrass)
    {
        IsTouchingGround = touchedGrass;

        ToggleAirDrag(!touchedGrass);
    }

    public bool IsGrounded()
    {
        return IsTouchingGround;
    }

    private void ToggleAirDrag(bool isInAir)
    {
        rb.drag = isInAir ? airDrag : groundDrag;

        StartCoroutine(DelayedFrictionChange(isInAir));

        
    }

    IEnumerator DelayedFrictionChange(bool isInAir)
    {
        yield return new WaitForSeconds(0.2f);
        physicMaterial.dynamicFriction = isInAir ? 0.0f : 0.6f;
        //Debug.Log(physicMaterial.dynamicFriction);
    }

    public void SpawnEnemies()
    {
        if (timeTillNextEnemySpawn > 0)
            return;

        timeTillNextEnemySpawn = enemySpawnCD;

        //EB.SpawnEnemy();
    }

    public IEnumerator KnockBack()
    {

        yield return new WaitForSeconds(1f);

    }

    void Update()
    {
        //// If they player is grounded and their y velocity is less than 0, then set the y velocity to 0.
        //groundedPlayer = controller.isGrounded;
        //if (groundedPlayer && playerVelocity.y < 0)
        //{
        //    playerVelocity.y = 0f;
        //}

        //// Get's the player movement and uses the new input system to set the character movements.
        //Vector2 movement = inputManager.GetPlayerMovement();
        //Vector3 move = new Vector3(movement.x, 0f, movement.y);
        //move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        //move.y = 0;
        //controller.Move(playerSpeed * Time.deltaTime * move);

        //// Changes the height position of the player.
        //if (inputManager.PlayerJumped() && groundedPlayer)
        //{
        //    playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        //}

        //playerVelocity.y += gravityValue * Time.deltaTime;
        //controller.Move(playerVelocity * Time.deltaTime);

        Dash();
        Kick();
        //Grapple();

        //Vector2 direction = Movement.ReadValue<Vector2>();
        //Vector3 move = new Vector3(direction.x, 0f, direction.y);
        //move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        //move.y = 0;
        //transform.position += new Vector3(direction.x, 0.0f, direction.y) * playerSpeed * Time.deltaTime;

        HandleWalkingSound();

        UpdateSoundPosition();
    }

    public void PlayDashSound()
    {
        PlaySound(ref DashSFXInstance, DashSFX);
    }
    public void PlayWalkingSound()
    {
        Debug.Log("Playing Walking Sound...");
        PlaySound(ref WalkingSFXInstance, WalkingSFX);
    }
    private void HandleWalkingSound()
    {
        if (isMoving && !isWalkingPlaying)
        {
            PlayWalkingSound();
            isWalkingPlaying = true;
        }
        else if (!isMoving && isWalkingPlaying)
        {
            StopWalkingSound();
            isWalkingPlaying = false;
        }
    }
    private void StopWalkingSound()
    {
        WalkingSFXInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
    private void PlaySound(ref EventInstance instance, EventReference eventRef)
    {
        instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        instance.release();
        instance = RuntimeManager.CreateInstance(eventRef);
        instance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        instance.start();
    }
    private void UpdateSoundPosition()
    {
        var attributes = RuntimeUtils.To3DAttributes(gameObject);
        DashSFXInstance.set3DAttributes(attributes);
        WalkingSFXInstance.set3DAttributes(attributes);
    }

    void Kick()
    {
        //if (!inputManager.PlayerKicked())
        //    return;

        //kickAbility.OnAbilityTrigger();
    }

    void Dash()
    {
        //if (!inputManager.PlayerDashed())
        //    return;

        //dashAbility.OnAbilityTrigger();
    }

    public IEnumerator PaddleBounce(int whichSide)
    {
        //"whichSide" is to see whether the collided paddle is on the right or left side of the ship.
        //This will effect the launch angle.
        // -1: right, 1: left
        switch (whichSide)
        {
            case -1:
                rb.velocity = Vector3.zero;
                yield return new WaitForSeconds(.3f);
                rb.velocity += new Vector3(-30, 70, 0);
                GetComponent<ParticleSystem>().Play();
                break;
            case 1:
                rb.velocity = Vector3.zero;
                yield return new WaitForSeconds(.3f);
                rb.velocity += new Vector3(30, 70, 0);
                GetComponent<ParticleSystem>().Play();
                break;
        }
    }


    private void OnDestroy()
    {
        Movement.started -= Movement_started;
        Movement.canceled -= Movement_canceled;
        Pause.started -= Pause_started;
        Pause.canceled -= Pause_canceled;
    }

    public void GameOverRestart()
    {
        SceneManager.LoadScene(sceneName: "VerticalSlice");
    }

    public void OnTriggerStay(Collider other)
    {
        
        if(other.gameObject.layer == 13)
        {

            if (FireScreen != null) { FireScreen.SetActive(false); }
            if (BlazeEffect != null) { BlazeEffect.SetActive(false); }

            if (lastAddedToTime > whenToAddTime)
            {

                HealthSystem.instance.FireDamage(1);
                lastAddedToTime = 0;

            }

        }

    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 13)
        {

            if (FireScreen != null) { FireScreen.SetActive(false); }
            if (BlazeEffect != null) { BlazeEffect.SetActive(false); }

        }
    }

    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(GetMoveInput(), slopeHit.normal).normalized;
    }

}

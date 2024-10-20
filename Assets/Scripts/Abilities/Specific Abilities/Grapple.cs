using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Grapple : MonoBehaviour
{
    private Vector3 hitPoint;
    private SpringJoint joint;
    private float maxDist = 100000000000f;

    public LayerMask GrappleLayer;
   // public LayerMask StingerLayer;
    //public LayerMask EnemyLayer;

    private LineRenderer hookRenderer;
    private Transform cam;

    public float GrappleTimer = 0;
    float maxGrappleTimer = 5;

    float whenToAddTime = 1;
    float lastAddedToTime = 0;

    public Image GrappleStamina;

    PlayerController playerController;



    [Tooltip("How much of the distance between the grapple hook and the player will be used.")]
    public float maxDistanceFromPointMultiplier = 0.25f;

    [Tooltip("Multiplier between the min distance between the player and the hook point.")]
    public float minDistanceFromPointMultiplier = 0.25f;

    [Tooltip("Spring to keep the player moving towards the target.")]
    public float jointSpring = 50f;

    [Tooltip("Damper force of the joint.")]
    public float jointDamper = 7f;

    [Tooltip("Spring for when player is holding space")]
    public float jumpingSpring = 9;

    [Tooltip("Damper for when player is holding space")]
    public float jumpingDamper = 4f;

    [Tooltip("Scalar for mass of the player and potential connected objects.")]
    public float jointMassScale = 4.5f;

    [Tooltip("Force to send the player in the direction of the grapple.")]
    public float jointForceBoost = 100f;

    private Rigidbody rb;

    public bool IsGrappling;
    bool canGrapple = true;

    [SerializeField] private Material BodyMaterial;

    [SerializeField] private Transform gunModel, gunFirePoint, gunFollowPoint, gunExitPoint;
   // [SerializeField] private Transform Head;

    private void Start()
    {
        cam = Camera.main.transform;
        rb = GetComponent<Rigidbody>();

        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        hookRenderer = gameObject.AddComponent<LineRenderer>();
        hookRenderer.positionCount = 2;
        hookRenderer.material = BodyMaterial;
        hookRenderer.startWidth = 1;
        hookRenderer.endWidth = 1;
        hookRenderer.textureMode = LineTextureMode.Tile;

       // GrappleStamina.fillAmount = 0;

    }

    void LateUpdate()
    {

        if (joint == null)
        {
            hookRenderer.positionCount = 0;
            gunModel.position = Vector3.MoveTowards(gunModel.position, gunExitPoint.position, Time.deltaTime * 2.5f);
            gunModel.rotation =
                Quaternion.RotateTowards(gunModel.rotation, gunExitPoint.rotation, Time.deltaTime * 2.5f);
        }
        else
        {
            gunModel.position = Vector3.MoveTowards(gunModel.position, gunFollowPoint.position, Time.deltaTime * 15f);
            gunModel.rotation =
                Quaternion.RotateTowards(gunModel.rotation, gunFollowPoint.rotation, Time.deltaTime * 15f);
            hookRenderer.positionCount = 2;
            hookRenderer.SetPosition(0, gunFirePoint.position);
            hookRenderer.SetPosition(1, hitPoint);

        }
    }


    private void Update()
    {
        lastAddedToTime += Time.deltaTime;

        if (playerController.IsTouchingGround == false && IsGrappling == true)
        {

            if(lastAddedToTime > whenToAddTime)
            {

                AddToTimer();
                lastAddedToTime = 0;

            }

        }

        if (playerController.IsTouchingGround == true && IsGrappling == false)
        {

            if (lastAddedToTime > whenToAddTime)
            {

                SubtractFromTimer();
                lastAddedToTime = 0;

            }

        }

    }

    public void StartGrapple()
    {

        if(canGrapple == false)
        {

            return;

        }

        // Debug.Log("yippee!!!!");

        if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, maxDist, GrappleLayer))
        {
            IsGrappling = true;

            GameObject.Find("PlayerViewmodel").GetComponent<Animator>().SetTrigger("Grapple");

            hitPoint = hit.point;
            joint = gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = hitPoint;

            float distanceFromPoint = Vector3.Distance(transform.position, hitPoint);

            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = distanceFromPoint * maxDistanceFromPointMultiplier;
            joint.minDistance = distanceFromPoint * minDistanceFromPointMultiplier;

            joint.spring = jointSpring;
            joint.damper = jointDamper;
            joint.massScale = jointMassScale;
            rb.AddForce((hitPoint - transform.position).normalized * jointForceBoost, ForceMode.Impulse);

        }
        
        /*
        if (Physics.Raycast(cam.position, cam.forward, out RaycastHit enemyHit, maxDist, EnemyLayer))
        {
            IsGrappling = true;

            hitPoint = enemyHit.point;
            joint = gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = hitPoint;

            float distanceFromPoint = Vector3.Distance(transform.position, hitPoint);

            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = distanceFromPoint * maxDistanceFromPointMultiplier;
            joint.minDistance = distanceFromPoint * minDistanceFromPointMultiplier;

            joint.spring = jointSpring;
            joint.damper = jointDamper;
            joint.massScale = jointMassScale;
            rb.AddForce((hitPoint - transform.position).normalized * jointForceBoost, ForceMode.Impulse);

            GameObject.FindObjectOfType<PlayerController>().CanBeHit = false;

        }

        if (Physics.Raycast(cam.position, cam.forward, out RaycastHit stingerHit, maxDist, StingerLayer))
        {
            IsGrappling = true;

            hitPoint = stingerHit.point;
            GameObject.FindObjectOfType<PlayerController>().CanBeHit = false;

        }
<<<<<<< HEAD
        */
        
    }

    public void StopGrapple()
    {

        IsGrappling = false;
        //GameObject.Find("PlayerViewmodel").GetComponent<Animator>().SetTrigger("EndGrapple");
        GameObject.FindObjectOfType<PlayerController>().CanBeHit = true;

        if (joint)

        {

            Destroy(joint);

        }

        StartCoroutine(GrapplingHookCooldown());

    }

    public void AddToTimer()
    {

        if(GrappleTimer != maxGrappleTimer)
        {

            GrappleTimer = GrappleTimer += 1; // * Time.deltaTime;
            GrappleStamina.fillAmount -= 0.20f;

        }
        else if (GrappleTimer >= maxGrappleTimer)
        {

            GameObject.Find("PlayerViewmodel").GetComponent<Animator>().SetTrigger("EndGrapple");
            StopGrapple();
            canGrapple = false;

        }

    }

    public void SubtractFromTimer()
    {

        if(GrappleTimer > 0)
        {

            GrappleTimer = GrappleTimer -= 1; // * Time.deltaTime;
            GrappleStamina.fillAmount += 0.20f;

        }
        else if (GrappleTimer == 0)
        {

            canGrapple = true;

        }

    }

    IEnumerator GrapplingHookCooldown()
    {

        canGrapple = false;

        yield return new WaitForSeconds(0.5f);

        canGrapple = true;

    }
}

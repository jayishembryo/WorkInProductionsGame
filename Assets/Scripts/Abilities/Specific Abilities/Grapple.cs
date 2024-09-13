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
    private float maxDist = 1000f;
    public LayerMask shootLayers;
    private LineRenderer hookRenderer;
    private Transform cam;

    float grappleTimer = 0;
    float maxGrappleTimer = 5;

    float whenToAddTime = 1;
    float lastAddedToTime = 0;

    public Image GrappleStamina;

    PlayerController playerController;



    [Tooltip("How much of the distance between the grapple hook and the player will be used.")]
    public float maxDistanceFromPointMultiplier = 0.8f;

    [Tooltip("Multiplier between the min distance between the player and the hook point.")]
    public float minDistanceFromPointMultiplier = 0.25f;

    [Tooltip("Spring to keep the player moving towards the target.")]
    public float jointSpring = 4.5f;

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

    public static bool isGrappling;
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


    private void FixedUpdate()
    {
        lastAddedToTime += Time.fixedDeltaTime;

        if (joint == null)
        {

            return;

        }

        if(playerController.IsTouchingGround == false)
        {

            joint.spring = jumpingSpring;
            joint.damper = jumpingDamper;

        }
        else
        {

            joint.spring = jointSpring;
            joint.damper = jointDamper;

        }

        if (playerController.IsTouchingGround == false && isGrappling == true)
        {

            if(lastAddedToTime > whenToAddTime)
            {

                AddToTimer();
                lastAddedToTime = 0;

            }

        }

        if (playerController.IsTouchingGround == true && isGrappling == false)
        {

            if(canGrapple == false && lastAddedToTime > whenToAddTime)
            {

                SubtractFromTimer();
                lastAddedToTime = 0;

            }

        }

        GrappleStamina.fillAmount = Mathf.Lerp(GrappleStamina.fillAmount, -(grappleTimer / maxGrappleTimer), 0.5f);

    }

    public void StartGrapple()
    {

        if(canGrapple == false)
        {

            return;

        }

        Debug.Log("yippee!!!!");

        if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, maxDist, shootLayers))
        {
            isGrappling = true;

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
    }

    public void StopGrapple()
    {
        isGrappling = false;
        if (joint)
        {
            Destroy(joint);
        }
    }

    public void AddToTimer()
    {

        if(grappleTimer != maxGrappleTimer)
        {

            grappleTimer = grappleTimer += 1;

        }
        else if (grappleTimer >= maxGrappleTimer)
        {

            StopGrapple();
            canGrapple = false;

        }

    }

    public void SubtractFromTimer()
    {

        if(grappleTimer > 0)
        {

            grappleTimer = grappleTimer -= 1;

        }
        else if (grappleTimer == 0)
        {

            canGrapple = true;

        }

    }
}

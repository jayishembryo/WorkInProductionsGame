using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachinePOVExtension : CinemachineExtension
{
    // Creating a clamp angle, this is how far up and down the player can see. I also created the horizontal and vertaical speeds of the camera.
    [SerializeField]
    private float clampAngle = 80f;
    [SerializeField]
    private float horizontalSpeed = 10f;
    [SerializeField]
    private float verticalSpeed = 10f;

    private InputManager inputManager;
    private Vector3 startingRotation;

    // Overriding the Awake function from InputManager and getting an instance of InputManager for inputManager.
    protected override void Awake()
    {
        inputManager = InputManager.Instance;
        base.Awake();
    }

    // Overriding the PostPipelineStageCallback function of Cinemachine/Virtual Camera. 
    // If we are at the Aim stage, we get the delta mouse input from the action map, and add the rotations onto x and y. 
    // This basically just messes with the amount the player can move around and see, as well as how fast they can do so. 
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (inputManager == null)
            return;

        if(vcam.Follow)
        {
            if(stage == CinemachineCore.Stage.Aim)
            {
                if(startingRotation == null)
                {
                    startingRotation = transform.localRotation.eulerAngles;
                }
                Vector2 deltaInput = inputManager.GetMouseDelta();
                startingRotation.x += deltaInput.x * verticalSpeed * Time.deltaTime;
                startingRotation.y += deltaInput.y * horizontalSpeed * Time.deltaTime;
                startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);
                state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);
            }
        }
    }
}

using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;

/// <summary>
/// A hybrid player controller that supports both VR and non-VR movement.
/// Automatically detects if VR is active and switches input and movement logic accordingly.
/// </summary>
public class HybridPlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Movement speed for both VR and non-VR players.")]
    public float moveSpeed = 2f;

    [Tooltip("Rotation speed for both VR and non-VR players.")]
    public float rotationSpeed = 45f;

    [Header("VR Settings")]
    [Tooltip("The VR headset (camera transform) used to determine forward movement direction.")]
    public Transform vrHead;

    [Tooltip("The root of the XR rig (for moving the whole player in VR).")]
    public Transform vrOrigin;

    [Tooltip("Automatically detects if using VR.")]
    public bool isUsingVR = false;

    [Header("Non-VR Settings")]
    [Tooltip("Character Controller used for movement in non-VR.")]
    public CharacterController characterController;

    [Tooltip("Main camera used for movement direction in non-VR mode.")]
    public Camera nonVRCamera;

    //Input values
    private Vector2 moveInput;
    private Vector2 rotateInput;

    //Input actions for movement and rotation
    private InputAction moveAction;
    private InputAction rotateAction;

    void Awake()
    {
        //Automatically checks if a VR headset is active
        isUsingVR = XRSettings.isDeviceActive;

        //Setup input actions using Unity's Input System
        var playerInput = new InputActionMap("Player");

        //VR controller bindings
        moveAction = playerInput.AddAction("Move", binding: "<XRController>{LeftHand}/primary2DAxis");
        rotateAction = playerInput.AddAction("Rotate", binding: "<XRController>{RightHand}/primary2DAxis/x");

        //Keyboard movement fallback
        moveAction.AddCompositeBinding("Dpad")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");

        //Keyboard rotation fallback
        rotateAction.AddBinding("<Keyboard>/q");
        rotateAction.AddBinding("<Keyboard>/e");

        //Enables the entire input map
        playerInput.Enable();
    }

    void Update()
    {
        //Reads current input values every frame
        moveInput = moveAction.ReadValue<Vector2>();
        rotateInput.x = rotateAction.ReadValue<float>();

        //Chooses movement method based on VR usage
        if (isUsingVR)
        {
            MoveVR();
        }
        else
        {
            MoveNonVR();
        }
    }

    /// <summary>
    ///Handles VR movement by moving the XR rig in the direction of the headset.
    /// </summary>
    void MoveVR()
    {
        if (vrOrigin == null || vrHead == null) return;

        //Calculates direction based on headset orientation
        Vector3 direction = vrHead.forward * moveInput.y + vrHead.right * moveInput.x;
        direction.y = 0f; // Flatten movement on XZ plane

        //Moves the entire rig
        vrOrigin.transform.position += direction * moveSpeed * Time.deltaTime;

        //Rotates the rig based on input
        vrOrigin.transform.Rotate(Vector3.up, rotateInput.x * rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Handles non-VR movement using a CharacterController and camera direction.
    /// </summary>
    void MoveNonVR()
    {
        if (characterController == null || nonVRCamera == null) return;

        //Calculates movement direction based on camera orientation
        Vector3 direction = nonVRCamera.transform.forward * moveInput.y + nonVRCamera.transform.right * moveInput.x;
        direction.y = 0f;

        //Moves using CharacterController
        characterController.Move(direction * moveSpeed * Time.deltaTime);

        //Rotates the player object (not the camera)
        transform.Rotate(Vector3.up, rotateInput.x * rotationSpeed * Time.deltaTime);
    }
}

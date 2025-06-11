using UnityEngine;
using UnityEngine.InputSystem;                
using UnityEngine.XR;                       
using System.Collections.Generic;
using XRInputDevice = UnityEngine.XR.InputDevice;

/// <summary>
/// Handles player interaction with notes using keyboard, controller, XR button input, or gaze.
/// Compatible with Meta XR (e.g. Quest), supports both VR and non-VR modes.
/// </summary>
public class NoteInteractionManager : MonoBehaviour
{
    [Header("Note Setup")]
    public float interactionDistance = 3f; //How far the player can interact
    public LayerMask interactionLayers;  //Layer for note objects
    public bool enableGaze = true;  //Toggles gaze-based interaction
    public float gazeTime = 2f;                   
    public InputAction readNoteAction; //Input Action for reading note (e.g. 'E' key)

    private Camera mainCamera;                     
    private float gazeTimer = 0f;  //Timer for gaze tracking
    private bool isUsingXR = false;   //Is the player in VR
    private XRInputDevice rightHandDevice; //XR controller reference
    private bool xrButtonPreviouslyPressed = false; //For XR button "pressed once" detection

    private void Awake()
    {
        mainCamera = Camera.main;

        //Registers callback for input action
        if (readNoteAction != null)
            readNoteAction.performed += OnReadNote;
    }

    private void OnEnable()
    {
        readNoteAction?.Enable();

        //Detects if XR is active
        isUsingXR = XRSettings.isDeviceActive;

        //Gets the right-hand XR controller device if available
        var devices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(XRNode.RightHand, devices);
        if (devices.Count > 0)
        {
            rightHandDevice = devices[0];
        }
    }

    private void OnDisable()
    {
        readNoteAction?.Disable();
    }

    private void Update()
    {
        //Casts a ray forward from the player's view
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, interactionDistance, interactionLayers))
        {
            //Gets a NoteUI component from the hit object
            NoteUI note = hit.collider.GetComponent<NoteUI>();

            if (note != null)
            {
                //Handles gaze-based interaction
                if (enableGaze)
                {
                    HandleGaze(note);
                }

                //Handles XR controller button input
                if (isUsingXR)
                {
                    HandleXRInput(note);
                }
            }
            else
            {
                ResetGaze(); //No note found, resets gaze timer
            }
        }
        else
        {
            ResetGaze(); //No object hit, resets gaze timer
        }
    }

    /// <summary>
    /// Handles interaction of head gaze (auto-reads after delay).
    /// </summary>
    private void HandleGaze(NoteUI note)
    {
        //Skips if already reading
        if (note == null || note.IsReading()) return;

        gazeTimer += Time.deltaTime;

        if (gazeTimer >= gazeTime)
        {
            note.ToggleLetter(); // Show note
            ResetGaze();
        }
    }

    /// <summary>
    ///Resets the gaze timer when not looking at a note.
    /// </summary>
    private void ResetGaze()
    {
        gazeTimer = 0f;
    }

    /// <summary>
    ///Handles keyboard/controller input via Input System when looking at a note.
    /// </summary>
    private void OnReadNote(InputAction.CallbackContext ctx)
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, interactionDistance, interactionLayers))
        {
            NoteUI note = hit.collider.GetComponent<NoteUI>();
            if (note != null)
            {
                note.ToggleLetter(); // Show/hide note
            }
        }
    }

    /// <summary>
    ///Handles VR controller input using XR CommonUsages (e.g., primary button).
    /// </summary>
    private void HandleXRInput(NoteUI note)
    {
        if (!rightHandDevice.isValid) return;

        //Checks if the primary button (e.g., A on Quest controller) is pressed
        if (rightHandDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out bool primaryButtonValue))
        {
            //Detects only the rising edge of the button press
            if (primaryButtonValue && !xrButtonPreviouslyPressed)
            {
                note.ToggleLetter(); //Show/hides note
            }

            //Stores button state for next frame
            xrButtonPreviouslyPressed = primaryButtonValue;
        }
    }
}

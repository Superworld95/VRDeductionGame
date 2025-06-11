using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This script displays a note in VR when the player enters a trigger zone,
/// shows a prompt to press a button (e.g., A on Oculus or E on keyboard).
/// Pressing the button shows the note, and pressing again hides it.
/// </summary>
public class NotePickup : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject noteUI;           
    public GameObject promptText;       
    public GameObject noteText;        

    private bool playerInRange = false; //Tracks if player is in the trigger zone
    private bool noteRead = false;      //Tracks if the note has already been read

    private NoteInputActions inputActions; 

    private void Awake()
    {
        //Creates a new input actions instance
        inputActions = new NoteInputActions();

        //Registers the input callback
        inputActions.NoteInteraction.ReadNote.performed += OnReadNote;
    }

    private void OnEnable()
    {
        //Enables input actions when this script becomes active
        inputActions.Enable();
    }

    private void OnDisable()
    {
        //Disables input actions to prevent unnecessary processing
        inputActions.Disable();
    }

    private void OnDestroy()
    {
        //Unsubscribes to avoid memory leaks
        inputActions.NoteInteraction.ReadNote.performed -= OnReadNote;
    }

    private void Start()
    {
        //Ensures all UI elements are hidden at the start
        noteUI?.SetActive(false);
        promptText?.SetActive(false);
        noteText?.SetActive(false);
    }

    // Called when the input action is triggered
    private void OnReadNote(InputAction.CallbackContext ctx)
    {
        if (!playerInRange) return;

        if (!noteRead)
            ShowNote();
        else
            HideNote();
    }

    // Called when something enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has entered the trigger zone
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            // Show the UI canvas and prompt
            noteUI?.SetActive(true);
            promptText?.SetActive(true);
        }
    }

    // Called when something exits the trigger collider
    private void OnTriggerExit(Collider other)
    {
        // Check if the player has exited the trigger zone
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            noteRead = false;

            // Hide all UI elements
            noteUI?.SetActive(false);
            promptText?.SetActive(false);
            noteText?.SetActive(false);
        }
    }

    // Show the note text and hide the prompt
    private void ShowNote()
    {
        noteRead = true;
        promptText?.SetActive(false);
        noteText?.SetActive(true);
    }

    // Hide the note text and show the prompt again
    private void HideNote()
    {
        noteRead = false;
        promptText?.SetActive(true);
        noteText?.SetActive(false);
    }
}

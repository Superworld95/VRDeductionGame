using UnityEngine;

/// <summary>
///Handles the logic for displaying/hiding the note UI and toggling player control.
///Can be used in both VR and non-VR setups.
/// </summary>
public class NoteUI : MonoBehaviour
{
    [Header("Note Components")]
    [Tooltip("Prefab of the UI that shows the note content.")]
    public GameObject noteUIPrefab;

    [Tooltip("Renderer of the 3D note object in the world.")]
    public Renderer noteMesh;

    [Tooltip("Player movement or controller script that should be disabled when reading the note.")]
    public MonoBehaviour playerController;

    //Instance of the spawned note UI (to avoid duplicates)
    private GameObject spawnedNoteUI;

    //Keeps track of whether the player is currently reading the note
    private bool isReading = false;

    /// <summary>
    ///Toggles the note open/close state.
    ///When opened, disables player movement and shows the UI.
    ///When closed, re-enables player movement and hides the UI.
    /// </summary>
    public void ToggleLetter()
    {
        isReading = !isReading;

        if (isReading)
        {
            //Spawns the UI if it hasn't been instantiated already
            if (spawnedNoteUI == null && noteUIPrefab != null)
                spawnedNoteUI = Instantiate(noteUIPrefab);

            //Shows the UI
            if (spawnedNoteUI != null)
                spawnedNoteUI.SetActive(true);

            //Hides the 3D mesh of the note
            if (noteMesh != null)
                noteMesh.enabled = false;

            //Disables player movement or interaction
            if (playerController != null)
                playerController.enabled = false;
        }
        else
        {
            //Hides the UI if it exists
            if (spawnedNoteUI != null)
                spawnedNoteUI.SetActive(false);

            //Re-enables the 3D mesh of the note
            if (noteMesh != null)
                noteMesh.enabled = true;

            //Re-enables player movement
            if (playerController != null)
                playerController.enabled = true;
        }
    }

    /// <summary>
    ///Returns whether the player is currently reading the note.
    /// </summary>
    public bool IsReading()
    {
        return isReading;
    }
}

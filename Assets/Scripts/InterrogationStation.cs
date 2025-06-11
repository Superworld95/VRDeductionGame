using Meta.XR.ImmersiveDebugger.UserInterface.Generic;
using TMPro;
using UnityEngine;

public class InterrogationStation : MonoBehaviour
{
    [SerializeField] public SuspectData assignedSuspect;  // Drag your SuspectData asset here
    public TextMeshProUGUI dialogueText;

    public void Start()
    {
        print(assignedSuspect.alibi);
        Debug.Log($"Assigned suspect: {assignedSuspect.suspectName}, Alibi: {assignedSuspect.alibi}");
        Debug.Log($"[Station] Assigned suspect: {assignedSuspect.suspectName}, ID: {assignedSuspect.GetInstanceID()}");

    }

    public void ToggleAlibi(bool isOn)
    {
        if (!isOn) return;

        if (assignedSuspect != null)
            dialogueText.text = "eat bums";//assignedSuspect.alibi;
        print("button pressed");
    }

    public void ToggleMotive(bool isOn)
    {
        if (!isOn) return;

        if (assignedSuspect != null)
            dialogueText.text = assignedSuspect.motive;
    }

    public void InterrogateAlibi()
    {
        if (assignedSuspect != null)
        {
            dialogueText.text = assignedSuspect.alibi;//assignedSuspect.alibi;
            print("button pressed");
        }
        //dialogueText.text = assignedSuspect.alibi;
        //dialogueText.text = "eat bums";//assignedSuspect.alibi;
        print("button pressed");

    }

    public void InterrogateMotive()
    {
        if (assignedSuspect != null)
            dialogueText.text = assignedSuspect.motive;
    }

    public void ToggleInterrogateAll(bool isOn)
    {
        if (assignedSuspect != null)
        {
            dialogueText.text = $"Name: {assignedSuspect.suspectName}\nAlibi: {assignedSuspect.alibi}\nMotive: {assignedSuspect.motive}";
        }
    }

    public void InterrogateAll()
    {
        if (assignedSuspect != null)
        {
            dialogueText.text = $"Name: {assignedSuspect.suspectName}\nAlibi: {assignedSuspect.alibi}\nMotive: {assignedSuspect.motive}";
        }
    }
}
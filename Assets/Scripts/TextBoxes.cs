using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TextBoxes : MonoBehaviour
{
    public TMP_Text dialogueBox, promptABox, promptBBox, promptCBox;
    public Button buttonA, buttonB, buttonC;
    public AudioSource textBlipSFX, pressButtonASFX, pressButtonBSFX, pressButtonCSFX;

    private InputAction nextAction, previousAction, interactAction;
    private InputActionAsset inputActions;

    private string[] dialogueLines = new string[]
    {
        "Welcome to the interrogation room. You have three suspects to question: Brad, Sara, and Tom."
    };
    private int currentLine = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;

    private enum DialogueState { Dialogue, Choices }
    private DialogueState state = DialogueState.Dialogue;

    private int selectedButtonIndex = 0;
    private Button[] buttons;
    private AudioSource[] buttonSFX;
    private Color defaultButtonColor;

    void Awake()
    {
        inputActions = Resources.Load<InputActionAsset>("InputSystem_Actions");
        nextAction = inputActions.FindAction("Next");
        previousAction = inputActions.FindAction("Previous");
        interactAction = inputActions.FindAction("Interact");

        buttons = new Button[] { buttonA, buttonB, buttonC };
        buttonSFX = new AudioSource[] { pressButtonASFX, pressButtonBSFX, pressButtonCSFX };

        if (buttonA != null)
            defaultButtonColor = buttonA.colors.normalColor;
    }

    void OnEnable()
    {
        nextAction.Enable();
        previousAction.Enable();
        interactAction.Enable();

        nextAction.performed += OnNextPerformed;
        previousAction.performed += OnPreviousPerformed;
        interactAction.performed += OnInteractPerformed;

        ShowNextLine();
    }

    void OnDisable()
    {
        nextAction.performed -= OnNextPerformed;
        previousAction.performed -= OnPreviousPerformed;
        interactAction.performed -= OnInteractPerformed;

        nextAction.Disable();
        previousAction.Disable();
        interactAction.Disable();
    }

    private void OnNextPerformed(InputAction.CallbackContext context)
    {
        if (state == DialogueState.Choices)
        {
            selectedButtonIndex = (selectedButtonIndex + 1) % buttons.Length;
            UpdateButtonSelection();
        }
        else if (!isTyping)
        {
            ShowNextLine();
        }
    }

    private void OnPreviousPerformed(InputAction.CallbackContext context)
    {
        if (state == DialogueState.Choices)
        {
            selectedButtonIndex = (selectedButtonIndex - 1 + buttons.Length) % buttons.Length;
            UpdateButtonSelection();
        }
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueBox.text = dialogueLines[currentLine - 1];
            isTyping = false;
            return;
        }

        if (state == DialogueState.Dialogue)
        {
            ShowNextLine();
        }
        else if (state == DialogueState.Choices)
        {
            buttonSFX[selectedButtonIndex]?.Play();
            switch (selectedButtonIndex)
            {
                case 0: SelectOption("Accept", "You accepted the offer.", "Good luck with your mission."); break;
                case 1: SelectOption("Decline", "You declined. Interesting choice.", "Let's see how this plays out."); break;
                case 2: SelectOption("Maybe Later", "Undecided, huh?", "Take your time, but time is limited."); break;
            }
        }
    }

    private void ShowNextLine()
    {
        if (currentLine < dialogueLines.Length)
        {
            if (typingCoroutine != null) StopCoroutine(typingCoroutine);
            typingCoroutine = StartCoroutine(TypeLine(dialogueLines[currentLine]));
            currentLine++;
        }
        else
        {
            EnterChoices();
        }
    }

    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueBox.text = "";

        foreach (char c in line)
        {
            dialogueBox.text += c;
            if (textBlipSFX != null) textBlipSFX.Play();
            yield return new WaitForSeconds(0.05f);
        }

        isTyping = false;
    }

    private void EnterChoices()
    {
        state = DialogueState.Choices;
        selectedButtonIndex = 0;
        UpdateButtonSelection();

        SetChoicesActive(true);

        promptABox.text = "Option A: Accept";
        promptBBox.text = "Option B: Decline";
        promptCBox.text = "Option C: Maybe Later";
    }

    private void HideChoices()
    {
        state = DialogueState.Dialogue;
        SetChoicesActive(false);
        promptABox.text = "";
        promptBBox.text = "";
        promptCBox.text = "";
    }

    private void SetChoicesActive(bool active)
    {
        foreach (var btn in buttons)
            if (btn != null)
                btn.gameObject.SetActive(active);
    }

    private void UpdateButtonSelection()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i] == null) continue;
            ColorBlock colors = buttons[i].colors;
            colors.normalColor = (i == selectedButtonIndex) ? Color.yellow : defaultButtonColor;
            buttons[i].colors = colors;
        }
    }

    private void SelectOption(string logMessage, string line1, string line2)
    {
        Debug.Log($"Option selected: {logMessage}");
        HideChoices();
        dialogueLines = new string[] { line1, line2 };
        currentLine = 0;
        ShowNextLine();
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class TextBoxes : MonoBehaviour
{
    public TMP_Text dialogueBox, promptABox, promptBBox, promptCBox;
    float textCrawl = 0, textCrawlC = 0, textSpeed = 0.4f, textSpeedC = 0.4f;
    string textReference = "", scrollingText = "";
    public Button buttonA, buttonB, buttonC;
    bool promptsActive = false;
    int pressedButtonID = 0;
    public AudioSource textBlipSFX, pressButtonASFX, pressButtonBSFX, pressButtonCSFX;
    int textBlipInterval1 = 0, textBlipInterval2 = 0;
    //bool promptAPressed = false, promptBPressed = false, promptCPressed = false;

    int dialogueTextID = 0, dialoguePathID = 0, dialoguePathID2 = 0; //Use these if you plan to fit all of the dialogue in this script.
     
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueBox.text = "";
        promptABox.text = "";
        promptBBox.text = "";
        promptCBox.text = "";
        dialogueBox.gameObject.SetActive(false);
        buttonA.gameObject.SetActive(false);
        buttonB.gameObject.SetActive(false);
        buttonC.gameObject.SetActive(false);

        if (buttonA != null)
        {
            buttonA.onClick.AddListener(promptAPressed);
        }
        if (buttonB != null)
        {
            buttonB.onClick.AddListener(promptBPressed);
        }
        if (buttonB != null)
        {
            buttonB.onClick.AddListener(promptCPressed);
        }

        //TEXT TEST. Remove when unneeded.
        dialogueBox.gameObject.SetActive(true);
        WriteText("Testing to see if this text scrolls, one character at a time, " +
            "depending on the scroll speed. Using inputs it is possible to speed up or skip text, " +
            "though I don't know the VR controller inputs so all of the code reliant on it is commented in the script. " +
            "Change the script and add anything to make each prompt work; you can fit all dialogue in the TextBoxes script. " +
            "The TextBoxes script is on the Canvas itself." +
            "You can even add a text speed setting from the menu if you want. I don't have an idea of how to do text pauses, unfortunately. " +
            "You could, I suppose, if you were insanely meticulous. At every textCrawl count you COULD switchcase " +
            "every single dialogueTextID and nested prompt ID and have a counter to determine exactly when there should be pauses and another varaible for how long the pauses are." +
            "I don't recommend this solution though. And hey, this has successfully tested the size of the text box. If you want less text, " +
            "you can of course resize the text box.");
        buttonA.gameObject.SetActive(true);
        buttonB.gameObject.SetActive(true);
        buttonC.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (textCrawl > 0)
        {
            textCrawl -= textSpeed;
            scrollingText = textReference.Substring(0, Mathf.FloorToInt(textCrawlC - textCrawl));
            dialogueBox.text = scrollingText;

            //Add a sound if you want to.
            /*
            textBlipInterval2 = textBlipInterval1;
            textBlipInterval1 = Mathf.FloorToInt(textCrawlC - textCrawl);
            if (textBlipInterval1 != textBlipInterval2)
            {
                textBlipSFX.Play();                
            }*/
        }



        //Add inputs here. The first is to speed up text, the second is to fill out the text.

        if (textCrawl > 0)
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.A))
            {
                textSpeed = 2 * textSpeedC;
            }
            else
            {
                textSpeed = textSpeedC;
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.S))
            {
                textCrawl = 1;
            }
        }
        else
        {

            //If the text has concluded, the same inputs should advance to the next text, unless there are prompts of course.
            if ((UnityEngine.Input.GetKeyDown(KeyCode.A) || UnityEngine.Input.GetKeyDown(KeyCode.S)) && !promptsActive)
            {

                dialogueTextID++;

                switch (dialogueTextID) //Your conditionals for all the text can occur here such as a counter.
                {
                    case 0: //In each case you have to change all prompts. Either write to or hide/unhide each of these.
                        WriteText("INSERT DIALOGUE HERE");
                        HideDialogue(false);
                        WritePromptA("");
                        HidePromptA(true);
                        WritePromptB("");
                        HidePromptB(true);
                        WritePromptC("");
                        HidePromptC(true);
                        promptsActive = false;
                        break;
                    case 1:
                        WriteText("YOU CAN HAVE DIALOGUE AND PROMPTS AT THE SAME TIME");
                        HideDialogue(false);
                        WritePromptA("Oh");
                        HidePromptA(false);
                        WritePromptB("Eh");
                        HidePromptB(false);
                        WritePromptC("That's boring!");
                        HidePromptC(false);
                        promptsActive = true;
                        break;
                    case 2:
                        WriteText("YOU CAN HAVE DIALOGUE AND PROMPTS AT THE SAME TIME");
                        HideDialogue(false);
                        WritePromptA("Oh");
                        HidePromptA(false);
                        WritePromptB("Eh");
                        HidePromptB(false);
                        WritePromptC("That's boring!");
                        HidePromptC(false);
                        promptsActive = true;
                        break;
                    case 3: //If there were prompts in the PREVIOUS message, they are checked for here to display different text.
                        switch (pressedButtonID)
                        {
                            case 0: WriteText("This text should not appear."); break;
                            case 1:
                                WriteText("INSERT PROMPT A RESPONSE HERE"); //You can also do things to other variables like obtaining items bools or anything.
                                HideDialogue(false); //You can do nested prompts, just set the dialoguePathID and track that in a nested switchcase in the next messages.
                                WritePromptA("");
                                HidePromptA(true);
                                WritePromptB("");
                                HidePromptB(true);
                                WritePromptC("");
                                HidePromptC(true);
                                promptsActive = false;
                                break;
                            case 2:
                                WriteText("INSERT PROMPT B RESPONSE HERE");
                                HideDialogue(false);
                                WritePromptA("");
                                HidePromptA(true);
                                WritePromptB("");
                                HidePromptB(true);
                                WritePromptC("");
                                HidePromptC(true);
                                promptsActive = false;
                                break;
                            case 3:
                                WriteText("INSERT PROMPT C RESPONSE HERE");
                                HideDialogue(false);
                                WritePromptA("");
                                HidePromptA(true);
                                WritePromptB("");
                                HidePromptB(true);
                                WritePromptC("");
                                HidePromptC(true);
                                promptsActive = false;
                                break;
                        }
                        break;
                    case 4:

                        switch (pressedButtonID)
                        {
                            case 0: WriteText("This text should not appear."); break;
                            case 1:
                                switch (dialoguePathID)
                                {
                                    case 1: //Copy over all the settings, I'm leaving this blank to save space.
                                        break;
                                    case 2:
                                        break;
                                    case 3:
                                        break;
                                }
                                break;
                            case 2:
                                switch (dialoguePathID)
                                {
                                    case 1:
                                        break;
                                    case 2:
                                        break;
                                    case 3:
                                        break;
                                }
                                break;
                            case 3:
                                switch (dialoguePathID)
                                {
                                    case 1:
                                        break;
                                    case 2:
                                        break;
                                    case 3:
                                        break;
                                }
                                break;
                        }
                        break;
                    case 5:
                        pressedButtonID = 0; //Set pressedButtonID to 0 when you are DONE with a nested prompt. If you need a nested prompt within a nested prompt, you need another ID variable and so on.
                        break;
                    case 6: break;
                    case 7: break;


                }
            }
        }
    }

    public void WriteText(string textContents)
    {
        textCrawlC = textContents.Length;
        textCrawl = textCrawlC;
        textReference = textContents;
    }

    public void WritePromptA(string textContents)
    {
        promptABox.text = textContents;
    }
    public void WritePromptB(string textContents)
    {
        promptBBox.text = textContents;
    }
    public void WritePromptC(string textContents)
    {
        promptCBox.text = textContents;
    }
    public void SetTextSpeed(int speed) 
    {
        textSpeedC = speed;
    }

    public void HideDialogue(bool flip)
    {
        dialogueBox.gameObject.SetActive(flip);
    }
    public void HidePromptA(bool flip)
    {
        promptABox.gameObject.SetActive(flip);
    }
    public void HidePromptB(bool flip)
    {
        promptBBox.gameObject.SetActive(flip);
    }
    public void HidePromptC(bool flip)
    {
        promptCBox.gameObject.SetActive(flip);
    }
    public void promptAPressed()
    {
        if (pressedButtonID == 0) //If you want nested nested prompts, you have to nest this if-statement as well to set every split path.
        {
            pressedButtonID = 1;
        } else
        {
            dialoguePathID = 1;
        }
        promptsActive = false;
        //pressButtonASFX.Play();
    }
    public void promptBPressed()
    {
        if (pressedButtonID == 0)
        {
            pressedButtonID = 2;
        }
        else
        {
            dialoguePathID = 2;
        }
        promptsActive = false;
        //pressButtonBSFX.Play();
    }
    public void promptCPressed()
    {
        if (pressedButtonID == 0)
        {
            pressedButtonID = 3;
        }
        else
        {
            dialoguePathID = 3;
        }
        promptsActive = false;
        //pressButtonCSFX.Play();
    }

}

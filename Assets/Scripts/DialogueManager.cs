using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    //public GameObject cursorDisplay;
    [HideInInspector] public bool DialogueOn = false;

    public static DialogueManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Fixe this!" + gameObject.name);
        }
        else
        {
            instance = this;
        }
    }
    public GameObject dialogueBox;
    public Text dialogueName;
    public Text dialogueText;
    public Image dialoguePortrait;
    public float delay = 0.001f;

    Queue<DialogueBase.Info> dialogueInfo = new Queue<DialogueBase.Info>(); //our collection Info

    //options stuff
    private bool isDialogueOption;
    public GameObject dialogueOptionUI;
    private bool inDialogue;
    public GameObject[] optionsButtoms;
    private int optionsAmount;
    public Text questionText;

    private bool isCurrentlyTyping;
    private string completeText;

    private void Start()
    {
        dialogueInfo = new Queue<DialogueBase.Info>();
    }
    public void EnqueueDialogue(DialogueBase db)
    {
        if (inDialogue) return;
        inDialogue = true;

        dialogueBox.SetActive(true);
        dialogueInfo.Clear();

        OptionsParser(db);
  
        //manage cursor
        /*Cursor.visible = !DialogueOn;
        if (DialogueOn) { Cursor.lockState = CursorLockMode.None; }
        else { Cursor.lockState = CursorLockMode.Locked; }

        cursorDisplay.SetActive(DialogueOn);


        DialogueOn = !DialogueOn;*/

        foreach (DialogueBase.Info info in db.dialogueInfo)
        {
            dialogueInfo.Enqueue(info);
        }

        DequeuDialogue();
    }

    public void DequeuDialogue()
    {
        if (isCurrentlyTyping == true)
        {
            CompleteText();
            StopAllCoroutines();
            isCurrentlyTyping = false;
            return;

        }

        if (dialogueInfo.Count == 0)
        {
            EndOfDialogue();
            return;
        }

        DialogueBase.Info info = dialogueInfo.Dequeue();
        completeText = info.myText;

        dialogueName.text = info.myName;
        dialogueText.text = info.myText;
        dialoguePortrait.sprite = info.portrait;

        dialogueText.text = "";
        StartCoroutine(TypeText(info));
    }

    IEnumerator TypeText(DialogueBase.Info info)
    {
        isCurrentlyTyping = true;
        //dialogueText.text = ""; //delete
        foreach (char c in info.myText.ToCharArray())
        {
            yield return new WaitForSeconds(delay);
            dialogueText.text += c;
            //yield return null;    //delete
        }
        isCurrentlyTyping = false;
    }

    private void CompleteText() 
    {
        dialogueText.text = completeText;
    }
    public void EndOfDialogue()
    {
        dialogueBox.SetActive(false);
        inDialogue = false;
        OptionsLogic();
    }

    private void OptionsLogic()
    {
        if (isDialogueOption == true)
        {
            dialogueOptionUI.SetActive(true);
        }
    }

    public void CloseOptions()
    {
        dialogueOptionUI.SetActive(false);
    }

    public void OptionsParser(DialogueBase db)
    {
        if (db is DialogueOptions)
        {
            isDialogueOption = true;
            DialogueOptions dialogueOptions = db as DialogueOptions;
            optionsAmount = dialogueOptions.optionsInfo.Length;
            questionText.text = dialogueOptions.questionsText;
            for (int i = 0; i < optionsButtoms.Length; i++)
            {
                optionsButtoms[i].SetActive(false);
            }

            for (int i = 0; i < optionsAmount; i++)
            {
                optionsButtoms[i].SetActive(true); //ok
                optionsButtoms[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = dialogueOptions.optionsInfo[i].buttonName;
                EventHandler myEventHandler = optionsButtoms[i].GetComponent<EventHandler>(); //ok
                myEventHandler.eventHandler = dialogueOptions.optionsInfo[i].myEvent;
                if (dialogueOptions.optionsInfo[i].nextDialogue != null)
                {
                    myEventHandler.myDialogue = dialogueOptions.optionsInfo[i].nextDialogue;
                }
                else
                {
                    myEventHandler.myDialogue = null;
                }
            }
        }
        else
        {
            isDialogueOption = false;
        }
    }
    /*public void ShowOrHideCursor()
    {
        //manage cursor
        Cursor.visible = !DialogueOn;
        if (DialogueOn) { Cursor.lockState = CursorLockMode.Locked; }
        else { Cursor.lockState = CursorLockMode.None; }

        //cursorDisplay.SetActive(DialogueOn);


        DialogueOn = !DialogueOn;
    }*/

}

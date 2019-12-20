using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    /*public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    
    public GameObject continueButton;
    */
    public Text nameText;   //Name's perso
    public TextMeshProUGUI dialogueText; //our dialogue

    public GameObject dialogueGUI; //persoGui(help key for the dialogue begin)
    public Transform dialogueBoxGUI;//canvas

    public float letterDelay = 0.1f; //typing speed
    public float letterMultiplier = 0.5f;

    public KeyCode DialogueInput = KeyCode.F;//dialogue key

    public string Names;

    public string[] dialogueLines;

    public bool letterIsMultiplied = false;
    public bool dialogueActive = false;
    public bool dialogueEnded = false;
    public bool outOfRange = true;

    public AudioClip audioClip;
    AudioSource audioSource;

    void Start()
    {
       audioSource = GetComponent<AudioSource>();
       dialogueText.text = "";
        //StartCoroutine(Type());
    }
    
     void Update()
     {
       /*if (Input.GetKeyUp("k"))      
        {
            StartDialogue();
            //Debug.Log("okk");
        }
        if (textDisplay.text == sentences[index])
         {
             continueButton.SetActive(true);
         }*/
    }

    public void EnterRangeOfNPC()
    {
        outOfRange = false;
        dialogueGUI.SetActive(true);//pop up of dialogue
        if (dialogueActive == true)
        {
            dialogueGUI.SetActive(false);
        }
    }

    public void NPCName()
    {
        outOfRange = false;
        dialogueBoxGUI.gameObject.SetActive(true);
        nameText.text = Names;
        if (Input.GetKeyDown(KeyCode.F))            //for the dialogue who continue: press again and again the key F
        {
            if (!dialogueActive)
            {
                dialogueActive = true;
                StartCoroutine(StartDialogue());
            }
        }
        StartDialogue();
    }
    private IEnumerator StartDialogue()
    {
        if (outOfRange == false)
        {
            int dialogueLength = dialogueLines.Length;
            int currentDialogueIndex = 0;
            //optButton01.SetActive(true);
            //optButton02.SetActive(true); 
            while (currentDialogueIndex < dialogueLength || !letterIsMultiplied)
            {
                if (!letterIsMultiplied)
                {
                    letterIsMultiplied = true;
                    StartCoroutine(DisplayString(dialogueLines[currentDialogueIndex++]));

                    if (currentDialogueIndex >= dialogueLength)
                    {
                        dialogueEnded = true;
                    }
                }
                yield return 0;
            }

            while (true)
            {
                if (Input.GetKeyDown(DialogueInput) && dialogueEnded == false)
                {
                    break;
                }
                yield return 0;
            }
            dialogueEnded = false;
            Debug.Log("fini");
            dialogueActive = false;
            DropDialogue();
        }
    }

    private IEnumerator DisplayString(string stringToDisplay)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (outOfRange == false)
        {
            int stringLength = stringToDisplay.Length;
            int currentCharacterIndex = 0;

            dialogueText.text = "";

            while (currentCharacterIndex < stringLength)
            {
                dialogueText.text += stringToDisplay[currentCharacterIndex];
                currentCharacterIndex++;

                if (currentCharacterIndex < stringLength)
                {
                    if (Input.GetKey(DialogueInput))
                    {
                        yield return new WaitForSeconds(letterDelay * letterMultiplier);

                        if (audioClip) audioSource.PlayOneShot(audioClip, 0.5F);
                    }
                    else
                    {
                        yield return new WaitForSeconds(letterDelay);

                        if (audioClip) audioSource.PlayOneShot(audioClip, 0.5F);
                    }
                }
                else
                {
                    dialogueEnded = false;
                    break;
                }
            }
            while (true)
            {
                if (Input.GetKeyDown(DialogueInput))
                {
                    break;
                }
                yield return 0;
            }
            dialogueEnded = false;
            letterIsMultiplied = false;
            dialogueText.text = "";
        }
    }
    
    public void DropDialogue()
    {
        dialogueGUI.SetActive(false);
        dialogueBoxGUI.gameObject.SetActive(false);
    }

    public void OutOfRange()
    {
        outOfRange = true;
        if (outOfRange == true)
        {
            letterIsMultiplied = false;
            dialogueActive = false;
            StopAllCoroutines();
            dialogueGUI.SetActive(false);
            dialogueBoxGUI.gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class NPC : MonoBehaviour
{
    public DialogueSystem diaSystem;

    public Transform ChatBackGround;
    public Transform CharacterImage;
    public Transform NPCCharacter;
    public Transform OurPerso;

    private DialogueSystem dialogueSystem;

    public string Name;
    public string NamePerso;

    [TextArea(5, 10)]
    public string[] sentences;

    //public GameObject[] buttons;
    public GameObject button1;
    public GameObject button2;

    void Start()
    {
        dialogueSystem = FindObjectOfType<DialogueSystem>();
    }

    void Update()
    {
        Vector3 Pos = Camera.main.WorldToScreenPoint(NPCCharacter.position);
        Pos.y += 175;
        ChatBackGround.position = Pos;
    }

    public void OnTriggerStay(Collider other)
    {
        this.gameObject.GetComponent<NPC>().enabled = true;
        FindObjectOfType<DialogueSystem>().EnterRangeOfNPC();
        if ((other.gameObject.tag == "Player") && Input.GetKeyDown(KeyCode.F))
        {
            CharacterImage.gameObject.SetActive(true);
            this.gameObject.GetComponent<NPC>().enabled = true;
            dialogueSystem.Names = Name;
            dialogueSystem.dialogueLines = sentences;
            FindObjectOfType<DialogueSystem>().NPCName();
        }
    }

    public void OnTriggerExit()
    {
        FindObjectOfType<DialogueSystem>().OutOfRange();
        this.gameObject.GetComponent<NPC>().enabled = false;
        CharacterImage.gameObject.SetActive(false);
    }
}


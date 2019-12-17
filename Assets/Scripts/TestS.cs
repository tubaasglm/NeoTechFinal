using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestS : MonoBehaviour
{
    public DialogueBase dialogue;

    public void TriggerDialogue()//ok
    {
        DialogueManager.instance.EnqueueDialogue(dialogue);
        //Debug.Log("de");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TriggerDialogue();
        }
        /*if (Input.GetKeyDown(KeyCode.F))
        {
            var sleez = new int();
            sleez++;
            Debug.Log("sleez");
        }*/
    }
}

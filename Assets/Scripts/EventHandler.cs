using System.Collections;  //not using
using System.Collections.Generic; //not using
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EventHandler : MonoBehaviour, IPointerDownHandler  //detect the click on the obj
{
    public UnityEvent eventHandler;
    public DialogueBase myDialogue;

    //Detect current clicks on the button (the one with the script attached)
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        eventHandler.Invoke();
        DialogueManager.instance.CloseOptions();

        if(myDialogue != null)
        {
            DialogueManager.instance.EnqueueDialogue(myDialogue);
        }
    }

}


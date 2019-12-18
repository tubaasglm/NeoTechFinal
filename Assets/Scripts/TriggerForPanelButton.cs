﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerForPanelButton : MonoBehaviour      //  ITS WORK !
{
    public GameObject PanelChoice;

    public void GiveTheObject()
    {
        //Player: give the object to Capsule(Guston Father) -> destroy the button + the audio HAHAHA begin, TRY AGAIN ! -> collect again the objects to make precious object
        Debug.Log("Try again!");// Game Over
        Destroy(PanelChoice);
        //StartDialogue();

    }

    public void DontGiveObject()
    {
        //if player don't give the object -> continue the game

        Debug.Log("Continue the Game!");
        Destroy(PanelChoice);
        // Sad audio of the enemy + text who say continue the game
    }

    void OnTriggerStay(Collider other)
    {
        if (PanelChoice)
            if (Input.GetKeyDown(KeyCode.P))
            {
                PanelChoice.SetActive(true);
            }
    }
}

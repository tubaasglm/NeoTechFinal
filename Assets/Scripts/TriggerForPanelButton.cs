using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerForPanelButton : MonoBehaviour      //  ITS WORK !
{
    public GameObject PanelChoice;

    public AudioClip audioclipGM;
    AudioSource audioSourceGM;

    public void GiveTheObject()
    {
        Cursor.visible = true;
        //Player: give the object to Capsule(Guston Father) -> destroy the button + the audio HAHAHA begin, TRY AGAIN ! -> collect again the objects to make precious object
        SceneManager.LoadScene("Lose");
        Debug.Log("Try again!");// Game Over
        Destroy(PanelChoice);

        audioSourceGM = this.GetComponent<AudioSource>();
        audioSourceGM.Play();
        //StartDialogue();

    }

    public void DontGiveObject()
    {
        //if player don't give the object -> continue the game
        SceneManager.LoadScene("Win");
        Debug.Log("Continue the Game!");
        Destroy(PanelChoice);
        // Sad audio of the enemy + text who say continue the game
    }

    void OnTriggerStay(Collider other)
    {
        Cursor.visible = true;
        if (PanelChoice)
            if (Input.GetKeyDown(KeyCode.P))
            {
                PanelChoice.SetActive(true);
            }
    }
}

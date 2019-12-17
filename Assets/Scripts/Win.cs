using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    public bool won = false;

    private void OnTriggerEnter(Collider other)
    {


        if (other.name == "FPSController" || other.name == "FirstPersonCharacter")
        {
            won = true;
        }
    }
}

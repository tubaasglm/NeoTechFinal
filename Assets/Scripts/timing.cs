using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class timing : MonoBehaviour
{
    public GameObject textDisplay;

    public int SecondsLeft = 30;
    public bool takingAway = false;

    void Start()
    {
        textDisplay.GetComponent<Text>().text = "00:" + SecondsLeft + 1f;
    }

    void Update()
    {
        if (takingAway == false && SecondsLeft >= 0)
        {
            StartCoroutine(TimeTake());
        }
    }

    //Coroutine
    IEnumerator TimeTake()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        SecondsLeft -= 1;

        if (SecondsLeft < 10)
        {
            textDisplay.GetComponent<Text>().text = "00:0" + SecondsLeft;
        }
        else
        {
            textDisplay.GetComponent<Text>().text = "00:" + SecondsLeft;
        }
        takingAway = false;



    }
}

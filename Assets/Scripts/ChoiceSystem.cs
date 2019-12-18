using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceSystem : MonoBehaviour
{
    public GameObject TextBox; 

    public GameObject Choice01;
    public GameObject Choice02;

    public int ChoiceMade;

    private void OnTriggerEnter(Collider other)
    {
        
    }
    public void ChoiceOption01()
    {
        
        
        Cursor.visible = true;
        TextBox.GetComponent<Text>().text = "That's good stranger. Looks like you made the first choice";
        ChoiceMade = 1;
    }

    public void ChoiceOption02()
    {
        TextBox.GetComponent<Text>().text = "I'm still a stranger too. Looks like you chose the second option";
        ChoiceMade = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(ChoiceMade >= 1 )
        {
            Choice01.SetActive(false);
            Choice02.SetActive(false);
        }
    }
}

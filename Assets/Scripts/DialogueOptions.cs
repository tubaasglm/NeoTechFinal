using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Dialogue Options", menuName = "DialoguesOptions")]
public class DialogueOptions : DialogueBase
{
    [TextArea(2, 10)]
    public string questionsText;

    [System.Serializable]
    public class Options
    {
        public string buttonName;
        public DialogueBase nextDialogue;
        public UnityEvent myEvent;  //add event : a click or otther event
    }
    public Options[] optionsInfo;
    //

}

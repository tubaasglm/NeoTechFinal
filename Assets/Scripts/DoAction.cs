using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoAction : MonoBehaviour
{
    public bool needItem = false;
    [Header("If need item is true")]
    public string itemType;
    public string itemID;//let null in inventor if not necessary
    public string textWithoutItem;
    public string textWithoutRightIDOfItem;

    public void DoActionNow()
    {
        gameObject.SetActive(false);
    }
}

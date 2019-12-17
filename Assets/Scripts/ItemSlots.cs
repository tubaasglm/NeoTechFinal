using System.Collections;   // pas necessaire
using System.Collections.Generic;
using UnityEngine;  // necessaire
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlots : MonoBehaviour
{
    public Text textItem;
    public GameObject textDisplay;


    [Header("Item's Datas")]
    public string itemType;
    public string itemID;
    public Sprite itemSprite;
    public string itemDescription;
    public bool itemReusable;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().sprite = itemSprite; //modification de l'image dans image(script) dans unity
        textItem.text = itemDescription;
    }
    void OnEnable()
    {
        DisableText(); //appeller a chaque fois que l'object en question est activer. Inventaire activer la fonction Enable appeller.
    }
    public void ActiveText()//activer la description 
    {
        textDisplay.SetActive(true);
    }
    public void DisableText()//désactiver la description
    {
        textDisplay.SetActive(false);
    }

    public void Click(BaseEventData bed)
    {
        PointerEventData ped = (PointerEventData)bed; // ou = bed as PointerEventData
        //Debug.Log("Button : " + ped.button);

        if(ped.button == PointerEventData.InputButton.Left)
        {
            TakeItem();
        }
        else if (ped.button == PointerEventData.InputButton.Right)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<FPSsupport>().ActiveItemOptions(gameObject);
        }
    }

    void TakeItem()
    {
        GameObject player = GameObject.FindWithTag("Player");

        player.GetComponent<FPSsupport>().YouAreHoldingItem(this.gameObject, itemType, itemID, itemSprite, itemReusable);
    }
}

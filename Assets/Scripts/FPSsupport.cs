using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSsupport : MonoBehaviour
{
    public GameObject playerCam;
    private UnityStandardAssets.ImageEffects.Blur blur;
    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpsComp;

    public Text infoDisplay;
    private bool infoCoroutineIsRunnig = false;

    public float pickupRange = 3.0f;
    private GameObject objectInteract;

    [Header("Button' List")]
    public string inventoryButton;
    public string interactButton;

    [Header("Tags' List")]
    public string itemTag = "item";
    public string doActionTag = "DoAction";

    [Header("Crosshair's datas")]
    public string layerInteract = "Interact";
    public GameObject crosshairDisplay;
    public int defaultSize = 30;
    public int specialSize = 50;
    public Sprite defaultTexture;
    public Sprite interactTexture;
    private bool useSpecialeTexture = false;

    [Header("Inventory's Datas")]
    public GameObject inventoryCanvas;//inventory panel
    public GameObject inventoryItemOptions;
    [HideInInspector] public bool inventoryOn = false; //or private and its for the cursor too
    public Transform itemPrefabs;
    public Transform inventorySlots;
    public int slotsCount = 16;
    private bool holdingItem = false;
    private GameObject itemObjectHold;
    private string itemTypeHold;
    private string itemIDHold;
    private bool itemReusableHold;

    // Start is called before the first frame update
    void Start()
    {
        if (playerCam == null)
        {
            playerCam = GameObject.FindGameObjectWithTag("MainCamera");
        }
        blur = playerCam.GetComponent<UnityStandardAssets.ImageEffects.Blur>();
        blur.enabled = false;

        if (infoDisplay == null)
        {
            infoDisplay = GameObject.Find("InfoDisplay").GetComponent<Text>();
        }
        infoDisplay.text = "";
        fpsComp = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();

        if (crosshairDisplay == null)
        {
            crosshairDisplay = GameObject.Find("Crosshair");
        }
        crosshairDisplay.GetComponent<RectTransform>().sizeDelta = new Vector2(defaultSize, defaultSize);

        if (inventoryCanvas == null)
        {
            inventoryCanvas = GameObject.Find("Inventory");
        }
        inventoryCanvas.SetActive(false);

        if(inventoryItemOptions)
        {
            inventoryItemOptions = GameObject.Find("InventoryItemOptions");
        }
        inventoryItemOptions.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(inventoryButton)) //fonctionne bien
        {
            ShowOrHideInventory();

            if(holdingItem)
            {
                StopHoldingItem();
            }
        }

        if(Input.GetButtonDown(interactButton))
        {
            if(infoCoroutineIsRunnig)
            {
                infoDisplay.text = "";
                infoCoroutineIsRunnig = false;
            }
            if(holdingItem)
            {
                TryToUse();
            }
            else
            {
                TryToInteract();
            }
            
        }

        if (!useSpecialeTexture)
        {
            Ray ray = playerCam.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //rayon depuis le camera
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, pickupRange)) //si le rayon touche un collier
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer(layerInteract))  //si le collider en quest appartient à un obj avec lequel on peut intragir = item or doAction ou ...
                {
                    crosshairDisplay.GetComponent<Image>().sprite = interactTexture;
                }
                else // si on peut interagir avec la chose en quest(un mur for example)
                {
                    crosshairDisplay.GetComponent<Image>().sprite = defaultTexture;
                }


            }
            else //si le rayon ne touche rien
            {
                crosshairDisplay.GetComponent<Image>().sprite = defaultTexture;
            }
        }
    }

    void TryToInteract()
    {
        Ray ray = playerCam.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //rayon depuis le camera
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, pickupRange))
        {
            objectInteract = hit.collider.gameObject;
            Debug.Log(" Item picked!");

            if(objectInteract.tag == itemTag)
            {
                //pick up
                //check if inventory is full
                if(inventorySlots.childCount == slotsCount)
                {
                    //Debug.Log("Inventory is full!");
                    infoDisplay.text = "Inventory is full";     //Texte qui affiche si l'inventaire est full
                    StartCoroutine(waitAndEraseInfo());
                }
                //however
                else 
                {
                    //make disappear object
                    objectInteract.SetActive(false);

                    //integrate new item in inventory
                    Transform newItem;
                    newItem = Instantiate(itemPrefabs, Vector3.zero, Quaternion.identity) as Transform;
                    newItem.SetParent(inventorySlots, false);

                    //download datas of items in slots of inventory
                    ItemSlots itemInventory = newItem.GetComponent<ItemSlots>();
                    ItemVariables itemScene = objectInteract.GetComponent<ItemVariables>();
                    itemInventory.itemType = itemScene.itemType;
                    itemInventory.itemID = itemScene.itemID;
                    itemInventory.itemSprite = itemScene.itemSprite;
                    itemInventory.itemDescription = itemScene.itemDescription;
                    itemInventory.itemReusable = itemScene.itemReusable;

                }

            }

            if (objectInteract.tag == doActionTag)
            {
                if(!objectInteract.GetComponent<DoAction>().needItem)
                {
                    objectInteract.GetComponent<DoAction>().DoActionNow();      //il faut bien mettre le nom(type) et l'ID d'item ! Et ça doit être même type et même ID !
                }
                else
                {
                    //Debug.Log("You can't do that without item");    //or put a locked door song
                    infoDisplay.text = objectInteract.GetComponent<DoAction>().textWithoutItem;
                    StartCoroutine(waitAndEraseInfo());
                }
            }
            
        }
    }

    void TryToUse()
    {
        Ray ray = playerCam.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //rayon depuis le camera
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            objectInteract = hit.collider.gameObject;

            if(objectInteract.tag == doActionTag && objectInteract.GetComponent<DoAction>().needItem)
            {
                //if right item do it
                if (itemTypeHold == objectInteract.GetComponent<DoAction>().itemType) //right item type hold
                {
                    if(itemIDHold == objectInteract.GetComponent<DoAction>().itemID || objectInteract.GetComponent<DoAction>().itemID == null) //right item ID hold
                    {
                        //right item
                        objectInteract.GetComponent<DoAction>().DoActionNow();

                        if(!itemReusableHold)
                        {
                            Destroy(itemObjectHold);//s'il est bon il a etre destroy
                        }
                    }
                    else
                    {
                        // wrong item ID 
                        //Debug.Log("It's not the right ID of item");
                        infoDisplay.text = objectInteract.GetComponent<DoAction>().textWithoutRightIDOfItem;
                        StartCoroutine(waitAndEraseInfo());

                    }
                }
                else
                {
                    //wrong item type
                    Debug.Log("It's not the right type of item");
                    infoDisplay.text = "You can not use this object like this";
                    StartCoroutine(waitAndEraseInfo());
                } 
            }
        }
        StopHoldingItem();
    }

    public void YouAreHoldingItem(GameObject itemObject, string itemType, string itemID, Sprite itemSprite, bool itemReusable)
    {
        holdingItem = true;

        //quiter auto l'inventaire
        ShowOrHideInventory();

        //stockage des données importées
        itemObjectHold = itemObject;
        itemTypeHold = itemType;
        itemIDHold = itemID;
        itemReusableHold = itemReusable;

        //modifier curseur
        useSpecialeTexture = true;
        crosshairDisplay.GetComponent<Image>().sprite = itemSprite;
        crosshairDisplay.GetComponent<RectTransform>().sizeDelta = new Vector2(specialSize, specialSize);
    }

    void StopHoldingItem()
    {
        // you used the item so he is not in your hands anymore(go back to invetory or destroy
        holdingItem = false;
        // and reset crosshair
        useSpecialeTexture = false;
        crosshairDisplay.GetComponent<RectTransform>().sizeDelta = new Vector2(defaultSize, defaultSize);
    }
    void ShowOrHideInventory()//pour cacher ou montrer l'inventaire
    {
        //maage inventory & player
        inventoryCanvas.SetActive(!inventoryOn);
        blur.enabled = !inventoryOn;
        fpsComp.enabled = inventoryOn;

        //manage inventory options - when inventory goes off, inventory options goes too
        if (inventoryOn) { inventoryItemOptions.SetActive(false); } //or callback the fonction { DisableItemOptions(); }

        //manage cursor
        Cursor.visible = !inventoryOn;
        if (inventoryOn) { Cursor.lockState = CursorLockMode.Locked; }
        else { Cursor.lockState = CursorLockMode.None; }

        crosshairDisplay.SetActive(inventoryOn);


        inventoryOn = !inventoryOn;
    }

    public void ActiveItemOptions(GameObject itemSelected)
    {
        inventoryItemOptions.SetActive(true);
        itemObjectHold = itemSelected;
        //Access au(x) button(s)
        Transform buttonsOptions = inventoryItemOptions.transform.GetChild(0);
        //Placement des options
        buttonsOptions.position = Input.mousePosition;
    }

    public void DisableItemOptions()
    {
        inventoryItemOptions.SetActive(false);
    }

    public void DropItem()
    {
        Destroy(itemObjectHold);
        inventoryItemOptions.SetActive(false); //or callback the fonction { DisableItemOptions(); }
    }

    //Coroutines
    IEnumerator waitAndEraseInfo()
    {
        infoCoroutineIsRunnig = true;
        //wait some time
        yield return new WaitForSeconds(5); //secondes pour disparation du text
        //after waiting
        if(infoCoroutineIsRunnig)
        {
            infoDisplay.text = "";
            infoCoroutineIsRunnig = false;
        }
    }
}


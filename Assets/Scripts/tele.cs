using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using TMPro;

public class tele : MonoBehaviour
{
    public List<Transform> Points = new List<Transform>();
    public TMP_Dropdown myDropDown; 

    public GameObject FPS;
    bool Activation = true;

    private void Start()
    {
        Cursor.visible = false;

        for (int i = 0; i < transform.childCount; i++)
        {
            Points.Add(transform.GetChild(i));
            myDropDown.options.Add(new TMP_Dropdown.OptionData() { text = transform.GetChild(i).name });
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))//pour cursor
        {
            Activation = !Activation;
            if(Activation)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                FPS.GetComponent<FirstPersonController>().enabled = true;
            }
            else
            {
                FPS.GetComponent<FirstPersonController>().enabled = false;
                Cursor.visible = true;

            }
        }
       /* if (Input.GetKeyDown(KeyCode.T)) //fonctionne bien
        {
           // ShowOrHideInventory();

            
        }*/
    }

    public void Teleport()
    {
        FPS.transform.position = Points[myDropDown.value].position;
        FPS.transform.rotation = Points[myDropDown.value].rotation;
    }
}

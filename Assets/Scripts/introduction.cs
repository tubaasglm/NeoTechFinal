using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introduction : MonoBehaviour
{
    public GameObject panelIntro;

    public void Continue()
    {
        Destroy(panelIntro);
    }
    public void OnTriggerStay(Collider other)
    {
        Cursor.visible = true;
        if(panelIntro)
        {
            if(Input.GetKeyDown(KeyCode.L))
            {
                panelIntro.SetActive(true);
            }
        }
        
    }
}

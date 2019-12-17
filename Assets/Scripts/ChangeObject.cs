using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeObject : MonoBehaviour
{
    public Material[] material;
    private Renderer rend;

    public GameObject objPrecious;
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
    }

    /*private void OnCollisionEnter(Collision colli)
    {
        if(colli.gameObject.tag == "Box")
        {
            rend.sharedMaterial = material[1];
        }
        else
        {
            rend.sharedMaterial = material[2];
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Box")
        {
            rend.sharedMaterial = material[1];
            //Destroy(rend);
            //objPrecious =Resources.Load("Prefabs/objPrecious Variant") as GameObject;
        }
        /*else
        {
            rend.sharedMaterial = material[2];
        }*/
    }
}

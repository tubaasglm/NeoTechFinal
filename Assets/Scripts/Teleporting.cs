using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporting : MonoBehaviour
{
    /*public Transform Destination;
    public GameObject teleport;

    private void OnTriggerStay(Collider other)
    {
        other.transform.position = Destination.transform.position - Vector3.up * 180;
        Debug.Log("teleported!");
    }*/

    Vector3 destination;

    void OnTriggerEnter(Collider other)
    {
        if (this.name == "Portail1")
        {
            destination = GameObject.Find("Portail2").transform.position;
            Debug.Log("edd");
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (this.name == "Portail2")
        {
            destination = GameObject.Find("Portail1").transform.position;
            Debug.Log("dijfe");
        }

        other.transform.position = destination - Vector3.forward * 3;
        other.transform.Rotate(Vector3.up * 180);
    }

}

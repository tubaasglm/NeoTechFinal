using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickItem : MonoBehaviour
{
    public Transform TheDestination;
    //public bool o2 = false;

    void OnMouseDown()
    {
      GetComponent<BoxCollider>().enabled = false;
      GetComponent<Rigidbody>().useGravity = false;
      this.transform.position = TheDestination.position; //moving
      this.transform.parent = GameObject.Find("Pick").transform;
    }

    void OnMouseUp()
    {
        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<BoxCollider>().enabled = true;
    }
}

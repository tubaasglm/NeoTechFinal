using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "Event")]
public class EventBehavior : ScriptableObject
{
    public void TestEvent()//ok
    {
        Debug.Log("its Ok for JK");
    }

    public void TestEvent02()//ok
    {
        Debug.Log("its Ok for RM");
    }

    public void TestEvent03()//ok
    {
        Debug.Log("its Ok for JH");
    }

    public void TestEvent04()//ok
    {
        Debug.Log("its Ok for SG");
    }
}

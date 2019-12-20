using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treesSounds : MonoBehaviour
{
    //public GameObject;

    public AudioClip audioclipTrees;
    AudioSource audioSourceTrees;

    public void OnTriggerEnter(Collider other)
    {
        audioSourceTrees = this.GetComponent<AudioSource>();
        audioSourceTrees.Play();
    }
}

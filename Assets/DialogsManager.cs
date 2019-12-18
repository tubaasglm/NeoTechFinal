using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogsManager : MonoBehaviour {

	public PageableDialog pdPrefab;
	public Canvas canvas; // dialogs must be on some canvas

	public PageableDialog CreatePageableDialog(){
		return Instantiate(pdPrefab, canvas.transform);
	}

	private static DialogsManager instance;
	public static DialogsManager Instance() {
		if(!instance){
			instance = FindObjectOfType(typeof (DialogsManager)) as DialogsManager;
			if(!instance)
				Debug.Log("There need to be at least one active DialogsManager on the scene");
		}

		return instance;
	}
}

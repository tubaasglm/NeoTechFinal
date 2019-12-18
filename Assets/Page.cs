using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent (typeof(CanvasGroup))]
public class Page : MonoBehaviour {

	public Text pageText;
	private CanvasGroup cg;

	void Awake(){
		cg = GetComponent<CanvasGroup>();
	}

	public void Show(){
		this.cg.alpha = 1f;
		this.cg.interactable = true; // in case it's a scrollview or something and you want to scroll - it has to be interactable to allow clicks
		this.cg.blocksRaycasts = true;
	}

	public void Hide(){
		this.cg.alpha = 0f;
		this.cg.interactable = false;
		this.cg.blocksRaycasts = false;
	}
}

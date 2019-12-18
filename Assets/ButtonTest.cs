using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTest : MonoBehaviour {


	public PageableDialog dialogPrefab;



	public void Click(){

		PageableDialog dialog = DialogsManager.Instance().CreatePageableDialog();
		dialog.AddPage("Hello there 1");
		dialog.AddPage("Hello there 2");
		dialog.AddPage("Hello there 3");
		dialog.OnAccept("Accept!", () => { 
			Debug.Log("FINISHED");
			dialog.Hide();
		});
		dialog.SetTitle("Some NPC");
		dialog.SetSubtitle("Some quest");
		dialog.Show();

		
	}
}

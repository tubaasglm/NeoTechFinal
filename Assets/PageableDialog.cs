using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

[RequireComponent (typeof(CanvasGroup))]
public class PageableDialog : MonoBehaviour {

	// title and subtitle for the entire dialog, like NPC name and quest name or something
	public Text title, subtitle;

	// buttons and their text components (Prev/Next and on the last page - Accept)
	public Text nextText, prevText;
	public Button prevButton, nextButton;

	// accept text when we're on the last page of the dialog
	private string acceptText = "Accept Quest"; 

	// what happens when user clicks Accept
	private UnityAction onAcceptAction;

	// canvas group for the entire dialog
	private CanvasGroup cg;

	// prefab for the page object (it has just a text component)
	public Page pagePrefab;

	// list of pages (added through AddPage())
	public List<Page> pages = new List<Page>();

	// current page index 
	private int _currentPage;


	void Awake(){
		cg = GetComponent<CanvasGroup>();

		_currentPage = 0;

		// prevbutton always shows the previous page so (currentPage - 1)
		prevButton.onClick.AddListener(() => {
			if(_currentPage > 0){ // so we don't exceed the pages list
				ShowPage(_currentPage - 1);
			}
		});

		// nextButton always shows the next page (current + 1)
		nextButton.onClick.AddListener(() => {
			if(_currentPage < pages.Count - 1){ // so we don't exceed the pages list
				ShowPage(_currentPage + 1);
			}
		});
	}

	// you can customize this method and the Page class so it has some scrolls, buttons etc.
	// you can add different AddPage methods simultanously, one which adds a scrollview, one adds a text etc. 
	// then, customize the Page class
	public void AddPage(string pageText){
		Page page = Instantiate(pagePrefab, this.transform);
		page.pageText.text = pageText;
		pages.Add(page);

	}

	// what happens on the last page, when user clicks Accept
	public PageableDialog OnAccept(string text, UnityAction action){
		acceptText = text;
		onAcceptAction = action;
		return this;
	}

	public PageableDialog SetTitle(string title){
		this.title.text = title;
		return this;
	}

	public PageableDialog SetSubtitle(string subtitle){
		this.subtitle.text = subtitle;
		return this;
	}

	public void Hide(){
		cg.alpha = 0f;
		cg.interactable = false;
		cg.blocksRaycasts = false;

		// destroy or reset, better destroy cause we create dialogs in DialogManager
		// when we're finished, we destroy the dialog object. reset is not needed, you can remove it.
		Reset();
		Destroy(this.gameObject); 
	}

	// we show the entire dialog and then, show the first page instantly.
	// we also setasLastSibling to make the dialog on top of other things on canvas.
	public void Show(){
		this.transform.SetAsLastSibling();
		cg.alpha = 1f;
		cg.interactable = true;
		cg.blocksRaycasts = true;

		// show the 1st page

		if(pages.Count > 0){
			ShowPage(0);
		} else throw new Exception ("We have an error, m4te! Wrong implementation. You have to add pages to the dialog before showing it.");
	}

	public void ShowPage(int pageNum){

		// safety check
		if(pageNum < 0 || pageNum >= pages.Count)
			pageNum = 0;

		// we set the currentpage
		_currentPage = pageNum;

		// if we show page 0, we disable the "Prev" button
		if(_currentPage == 0) prevButton.gameObject.SetActive(false);
		else prevButton.gameObject.SetActive(true);

		// if we're showing the last page, we change the button's text to our accept text, like "Accept Quest" and add the OnAcceptAction to be performed when clicked.
		if(_currentPage == (pages.Count - 1)){
			nextText.text = acceptText;
			nextButton.onClick.AddListener(onAcceptAction);
		} 
		else // otherwise, we remove this listener or it would accept the quest on each page :D
		{
			nextText.text = "Next Page";
			nextButton.onClick.RemoveListener(onAcceptAction);
		}

		// finally, we iterate through pages, show the one we picked and hide the rest.
		for(int i=0;i<pages.Count;i++){
			if(i == pageNum){
				pages[i].Show();
			} else pages[i].Hide();
		}


	}

	// if you want to reuse dialogs, use this method to reset them, but don't reuse. 
	private void Reset(){
		foreach(Page p in pages){
			p.Hide();
		}

		_currentPage = 0;
	}

}

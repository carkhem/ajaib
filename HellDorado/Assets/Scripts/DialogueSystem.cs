using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour {
	private GameObject dialoguePanel;
	private Text uiText;
	private Text uiName;
	public string speakerName;
	public Color nameColor;
	public string[] pages;
	private int currentPage = 0;

	void Start(){
		dialoguePanel = CanvasManager.instance.dialoguePanel;
		uiText = CanvasManager.instance.dText;
		uiName = CanvasManager.instance.dName;
		CanvasManager.instance.dialoguePanel.SetActive (false);
	}

	public void StartDialogue(){
		if (!dialoguePanel.activeSelf) {
			dialoguePanel.SetActive (true);
			currentPage = 0;
			uiText.text = pages [currentPage];
			uiName.text = speakerName;
			uiName.color = nameColor;
		}
	}

	public void EndDialogue(){
		dialoguePanel.SetActive (false);
		currentPage = 0;
	}

	public void Update(){
		if (Input.GetKeyDown (KeyCode.E) && dialoguePanel.activeSelf) {
			if (pages.Length - 1 > currentPage) {
				currentPage += 1;
				uiText.text = pages [currentPage];
			} else {
				EndDialogue ();
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour {
	public GameObject dialogueBox;
	public Text uiText;
	public Text uiName;
	public string speakerName;
	public string[] pages;
	private int currentPage = 0;

	void Awake(){
		dialogueBox.SetActive (false);
	}

	public void StartDialogue(){
		dialogueBox.SetActive (true);
		currentPage = 0;
		uiText.text = pages [currentPage];
		uiName.text = speakerName;
	}

	public void Update(){
		if (Input.GetKeyDown (KeyCode.E) && dialogueBox.activeSelf) {
			if (pages.Length - 1 > currentPage) {
				print ("NextPage");
				currentPage += 1;
				uiText.text = pages [currentPage];
			} else {
				dialogueBox.SetActive (false);
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {
	public static CanvasManager instance;

	[Header("Dialogue")]
	public Text dText;
	public Text dName;
	public GameObject dialoguePanel;

	[Header("Stats")]
	public Slider healthSlider;
	public GameObject deathScreen;

	[Header("Ability Panel")]
	public GameObject abilityContent;
	public GameObject rewindPanel;

	void Awake(){
		instance = this;
	}

	void Start(){
		rewindPanel.SetActive (false);
		deathScreen.SetActive (false);
	}

	public void ChangeAbility(int currentAbility){
		for (int i = 0; i < abilityContent.transform.childCount; i++) {
			abilityContent.transform.GetChild (i).GetComponent<AbilityDisplay> ().SetActive (false);
		}
		abilityContent.transform.GetChild (currentAbility).GetComponent<AbilityDisplay> ().SetActive (true);
	}

	public void AddAbility(GameObject abilityDisplayPrefab){
		Instantiate (abilityDisplayPrefab, abilityContent.transform);
	}

}

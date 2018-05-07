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
    public GameObject LevelUpPicture;

	[Header("Stats")]
	public GameObject healthBar;
	public Slider healthSlider;
	public Text healthProcent;
	public GameObject deathScreen;
    public Slider experienceSlider;
    public Text experienceProgress;
    public Text PlayerLevelText;

	[Header("Ability Panel")]
	public GameObject abilityContent;
	public GameObject rewindPanel;

	[Header("Fighting")]
	public Slider enemyHealthSlider;
	private GameObject currentEnemy;

	[Header("Interact")]
	public Text interactText;

	void Awake(){
		instance = this;
	}

	void Start(){
		rewindPanel.SetActive (false);
		deathScreen.SetActive (false);
		ExitEnemySlider ();
	}

	void Update(){
		if (currentEnemy != null) {
			enemyHealthSlider.value = currentEnemy.GetComponent<EnemyController> ().GetHealthPercentage ();
		}
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

	public void SetEnemySlider(GameObject enemy){
		currentEnemy = enemy;
		enemyHealthSlider.gameObject.SetActive (true);
	}

	public void ExitEnemySlider(){
		enemyHealthSlider.gameObject.SetActive (false);
	}

}

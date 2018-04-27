using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager instance;

	public int playerLevel = 0;
	public GameObject[] abilityDisplay;

	void Awake(){
		//DONTDESTROYONLOAD! Det är ett krav
		instance = this;
	}

	void Start(){
		for (int i = 0; i < playerLevel; i++) {
			CanvasManager.instance.AddAbility (abilityDisplay[i]);
		}
	}

	public void LevelUp(){
		playerLevel++;
		if (abilityDisplay.Length >= playerLevel)
			CanvasManager.instance.AddAbility (abilityDisplay[playerLevel - 1]);
	}

	public void GameOver(){
		CanvasManager.instance.deathScreen.SetActive (true);
	}

}

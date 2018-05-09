﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public static GameManager instance;

	public GameObject[] abilityDisplay;
	private GameObject cameraController;
    public GameObject player;
	private PlayerStats stats;

	void Update(){
		if (Input.GetKeyDown (KeyCode.Keypad1)){
			SceneManager.LoadScene("Level1");
		}
		if (Input.GetKeyDown (KeyCode.Keypad2)){
			SceneManager.LoadScene("Level2");
		}
		if (Input.GetKeyDown (KeyCode.Keypad3)){
			SceneManager.LoadScene("Level3");
		}
	}

    void Awake(){
		//DONTDESTROYONLOAD! Det är ett krav
		instance = this;
		cameraController = Camera.main.transform.parent.gameObject;
	}

	void Start(){
		if (stats == null)
			stats = PlayerStats.instance;
		UpdateAbilityList();
    }

	public void UpdateAbilityList(){
		for (int i = 0; i < stats.playerLevel; i++) {
			if (abilityDisplay.Length > i)
				CanvasManager.instance.AddAbility (abilityDisplay[i]);
		}
	}

	public void GameOver(){
        Time.timeScale = 0f;
        CanvasManager.instance.deathScreen.SetActive (true);
		CanvasManager.instance.abilityContent.transform.parent.gameObject.SetActive(false);
		CanvasManager.instance.healthBar.SetActive (false);
		cameraController.GetComponent<FPSCamera>().SetConstraints(0, 0, 0, 0);
    } 

	public void ChangeLevel(string sceneName){
		SceneManager.LoadScene (sceneName);
	}

    public void Respawn(){
        Vector3 spawnPosition = new Vector3(0, 0, 0);
        spawnPosition = CheckPoint.GetActiveCheckPointPosition();
		CanvasManager.instance.healthBar.SetActive (true);
        player.GetComponent<PlayerStats>().health = 100;
        CanvasManager.instance.deathScreen.SetActive(false);
		cameraController.GetComponent<FPSCamera>().RemoveConstraints();
        player.transform.position = spawnPosition;
		if (spawnPosition == new Vector3 (0, 0, 0)) {
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		}
    }
}

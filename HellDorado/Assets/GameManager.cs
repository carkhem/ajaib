using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager instance;

	public int playerLevel = 0;
	public GameObject[] abilityDisplay;
    public GameObject abilityList;
    public GameObject healthSlider;
    public GameObject cam;
    public GameObject player;

    void Awake(){
		//DONTDESTROYONLOAD! Det är ett krav
		instance = this;
	}

	void Start(){
		for (int i = 0; i < playerLevel; i++) {
			CanvasManager.instance.AddAbility (abilityDisplay[i]);
		}
        player.GetComponent<PlayerStats>().PlayerLevel = playerLevel;
    }

	public void LevelUp(){
        playerLevel++;
        player.GetComponent<PlayerStats>().PlayerLevel = playerLevel;
       // player.GetComponent<PlayerStats>().changeDmg(playerLevel);
        if (abilityDisplay.Length >= playerLevel)
			CanvasManager.instance.AddAbility (abilityDisplay[playerLevel - 1]);
	}

    public void experienceChange( float exp)
    {
        player.GetComponent<PlayerStats>().playerExp += exp;
        if (player.GetComponent<PlayerStats>().playerExp >= player.GetComponent<PlayerStats>().LevelMaxExp)
            LevelUp();
    }

	public void GameOver(){
        Time.timeScale = 0f;
        CanvasManager.instance.deathScreen.SetActive (true);
        abilityList.SetActive(false);
        healthSlider.SetActive(false);
        cam.GetComponent<FPSCamera>().SetDead(true);
     
     
    }

    public void Respawn()
    {
        Vector3 spawnPosition = new Vector3(0, 0, 0);
        spawnPosition = CheckPoint.GetActiveCheckPointPosition();
        healthSlider.SetActive(true);
        abilityList.SetActive(true);
        player.GetComponent<PlayerStats>().health = 100;
        CanvasManager.instance.deathScreen.SetActive(false);
        cam.GetComponent<FPSCamera>().SetDead(false);
        player.transform.position = spawnPosition;

    }
}

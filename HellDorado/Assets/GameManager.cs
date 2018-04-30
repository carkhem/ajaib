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
        Time.timeScale = 0f;
        CanvasManager.instance.deathScreen.SetActive (true);
        abilityList.SetActive(false);
        healthSlider.SetActive(false);
        cam.GetComponent<FPSCamera>().SetDead(true);
     
     
    }
    public void Respawn()
    {
        GameObject player =  GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerStats>().health = 100;
        Time.timeScale = 1f;
        CanvasManager.instance.deathScreen.SetActive(false);
        abilityList.SetActive(true);
        healthSlider.SetActive(true);
        cam.GetComponent<FPSCamera>().SetDead(false);

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPTest : MonoBehaviour {

    public GameObject player;
    public GameObject deathScreen;
    public GameObject cam;
    public GameObject abilityList;
    public GameObject healthSlider;
	// Use this for initialization
	void Start () {
      
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Delete)){
            player.GetComponent<PlayerStats>().health = 0;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 1;
        }

        if (deathScreen.activeInHierarchy == true) { 
        Vector3 spawnPosition = new Vector3(0, 0, 0);
             if (Input.GetKeyDown(KeyCode.Return))
        {

                spawnPosition = CheckPoint.GetActiveCheckPointPosition();
                player.transform.position = spawnPosition;
                GameManager.instance.Respawn();
            }
    }
	}
}

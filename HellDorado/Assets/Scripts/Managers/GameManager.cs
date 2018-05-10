using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public static GameManager instance;

	public int playerLevel = 0;
	public float playerEXP;
	public GameObject[] abilityDisplay;
	private GameObject cameraController;
    public GameObject player;
	private PlayerStats stats;
    private Vector3 CheckPointPosition;
//	private CheckPoint checkpoint;

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
		if (Time.timeScale == 0) {
			if (Input.GetKeyDown (KeyCode.Return))
				Respawn ();
		}
	}

    void Awake(){
		DontDestroyOnLoad (gameObject);
		instance = this;
		cameraController = Camera.main.transform.parent.gameObject;
	}

	void Start(){
		if (stats == null)
			stats = PlayerStats.instance;
		UpdateAbilityList();
		player = stats.transform.gameObject;
    }

	public void UpdateAbilityList(){
		CanvasManager.instance.ClearAbilities ();
		for (int i = 1; i < playerLevel; i++) {
			if (abilityDisplay.Length + 1 > i)
				CanvasManager.instance.AddAbility (abilityDisplay[i - 1]);
		}
	}

	public void GameOver(){
        Time.timeScale = 0f;
        CanvasManager.instance.deathScreen.SetActive (true);
		CanvasManager.instance.abilityContent.transform.parent.gameObject.SetActive(false);
		CanvasManager.instance.healthBar.SetActive (false);
		cameraController.GetComponent<FPSCamera>().SetStatic(true);
		player.GetComponent<PlayerController> ().TransitionTo<DeathState> ();
    } 

	public void ChangeLevel(string sceneName){
		SceneManager.LoadScene (sceneName);
	}

    public void SetCheckPoint(Vector3 position){
        CheckPointPosition = position;
    }

    public void Respawn(){
		Time.timeScale = 1f;
		CanvasManager.instance.deathScreen.SetActive(false);
		CanvasManager.instance.abilityContent.transform.parent.gameObject.SetActive(true);
		CanvasManager.instance.healthBar.SetActive(true);
		stats.health = stats.maxHealth;
		cameraController.GetComponent<FPSCamera>().SetStatic(false);
		if (CheckPointPosition != null)
			player.transform.position = CheckPointPosition;
		else
			ChangeLevel (SceneManager.GetActiveScene ().name);
		EnemyRespawn();
    }

    private void EnemyRespawn()
    {
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].GetComponent<EnemyController>().CurrentState.name != "DeadState")
            {
                enemies[i].GetComponent<EnemyController>().TransitionTo<IdleState>();
                enemies[i].GetComponent<EnemyController>().detection = 0f;
                enemies[i].transform.position = enemies[i].GetComponent<EnemyController>().patrolPoints[0];
            }
        }
    }
}

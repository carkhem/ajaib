using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int playerLevel = 0;
    public float playerEXP;
	[HideInInspector]
	public float maxEXP {get {return (playerLevel < 1) ? 100  : playerLevel * 100;}}
	public int abilityCount = 0;
    public GameObject[] abilityDisplay;
    private GameObject cameraController;
    public GameObject player;
    private PlayerStats stats;
    private GameObject CheckPoint;
    private GameObject[] enemies;


	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		instance = this;
		cameraController = Camera.main.transform.parent.gameObject;
	}

	void Start()
	{
		if (stats == null)
			stats = PlayerStats.instance;
		UpdateAbilityList();
		player = stats.transform.gameObject;

		enemies = GameObject.FindGameObjectsWithTag("Enemy");
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            SceneManager.LoadScene("Level1");
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            SceneManager.LoadScene("Level2");
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            SceneManager.LoadScene("Level3");
        }
        if (Time.timeScale == 0)
        {
            if (Input.GetKeyDown(KeyCode.Return))
                Respawn();
        }
    }

    public void UpdateAbilityList()
    {
        CanvasManager.instance.ClearAbilities();
		for (int i = 0; i < abilityCount; i++)
        {
            if (abilityDisplay.Length > i)
                CanvasManager.instance.AddAbility(abilityDisplay[i]);
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        CanvasManager.instance.deathScreen.SetActive(true);
        CanvasManager.instance.abilityContent.transform.parent.gameObject.SetActive(false);
        CanvasManager.instance.healthBar.SetActive(false);
        cameraController.GetComponent<FPSCamera>().SetStatic(true);
        player.GetComponent<PlayerController>().TransitionTo<DeathState>();
    }

    public void ChangeLevel(string sceneName)
    {
        CheckPoint = null;
        SceneManager.LoadScene(sceneName);
    }

    public void SetCheckPoint(GameObject checkPoint)
    {
        CheckPoint = checkPoint;
    }

    public void Respawn()
    {
        Time.timeScale = 1f;
        CanvasManager.instance.deathScreen.SetActive(false);
        CanvasManager.instance.abilityContent.transform.parent.gameObject.SetActive(true);
        CanvasManager.instance.healthBar.SetActive(true);
        stats.health = stats.maxHealth;
        cameraController.GetComponent<FPSCamera>().SetStatic(false);
        if (CheckPoint != null)
        {
            player.transform.position = CheckPoint.GetComponent<CheckPoint>().GetPosition();
            CheckPoint.GetComponent<CheckPoint>().RespawnEvent();
        }
        else
        {
            ChangeLevel(SceneManager.GetActiveScene().name);
        }
        RespawnEnemies();
    }

    private void RespawnEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyController>().Respawn();
        }
    }

	public void NewAbility(){
		SetAbilityCount (abilityCount + 1);
	}

	public void SetAbilityCount(int ammount){
		abilityCount = ammount;
		UpdateAbilityList ();
		//HÄR SKA FEEDBACK GES FÖR NY ABILITY
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour {

    public GameObject[] minions;
    public int Health;
    public int phase;
    public GameObject boulderSpawner;
    public float spawnTimer;
    public GameObject player;
    private bool ressing;
    public GameObject fireBallShooter;


    // Use this for initialization
    void Start () {
        Health = 100;
        phase = 1;
        spawnTimer = 3;
        ressing = false;
        
	}
	
	// Update is called once per frame
	void Update () {
        CheckThatMinionsAreDead();
		
	}

    void ReviveMinions()
    {
        foreach(GameObject g in minions)
        {
            g.GetComponent<EnemyController>().Revive();
        }
    }

    void RestorePlayerHealth()
    {
        player.GetComponent<PlayerStats>().health = player.GetComponent<PlayerStats>().maxHealth;
    }
    void CheckThatMinionsAreDead()
    {
        
        foreach (GameObject g in minions)
        {
            //Kolla om mobsen lever
            if(g.GetComponent<EnemyController>().health > 0)
            {
                //Om mobsen lever, bryt
                return;
            }
            
        }
        //Om mobsen är döda, ingå nästa fas

        if (ressing == false)
        {
            Invoke("EnterNextPhase", spawnTimer);
            ressing = true;
        }

    }

    void Die()
    {
        Destroy(this.gameObject);
        
    }

    void EnterNextPhase()
    {

       
            //subtrahera HP med 30
            Health -= 30;

            //Kolla vilken fas man är i
            switch (phase)
            {
                case 1:
                    RestorePlayerHealth();
                    ReviveMinions();
                    fireBallShooter.SetActive(true);
                break;
                case 2:
                    RestorePlayerHealth();
                    ReviveMinions();
                    boulderSpawner.SetActive(true);
                    break;
                case 3:
                    fireBallShooter.SetActive(false);
                    boulderSpawner.SetActive(false);
                    Die();
                    break;
            }
       phase++;
       ressing = false;
    }


}

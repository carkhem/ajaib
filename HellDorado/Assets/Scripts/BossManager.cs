using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour {

    public GameObject[] minions;
    public int Health;
    public int phase;
    public GameObject boulderSpawner;
    public float spawnTimer;


    // Use this for initialization
    void Start () {
        Health = 100;
        phase = 1;
        spawnTimer = 3;
        
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


    void CheckThatMinionsAreDead()
    {
        
        foreach (GameObject g in minions)
        {
            //Kolla om mobsen lever
            if(g.GetComponent<EnemyController>().health > 0)
            {
                //Om mobsen lever, returnera falsk
                return;
            }
            
        }
        //Om mobsen är döda, ingå nästa fas

        Invoke("EnterNextPhase", 3);
       // EnterNextPhase();

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
                    ReviveMinions();
                    //Börja kasta eldbollar
                    break;
                case 2:
                    ReviveMinions();
                    Instantiate(boulderSpawner, this.transform.position, this.transform.rotation);
                    break;
                case 3:
                    Die();
                    break;
            }

        }

        
}

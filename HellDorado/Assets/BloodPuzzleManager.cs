using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPuzzleManager : MonoBehaviour {
    public GameObject[] Podiums;
    public GameObject enemy;
    public GameObject gate;
    public GameObject blood;
    public float spawnTimer;
    private bool ressing;
    private bool gateOpen;

	// Use this for initialization
	void Start () {
        ressing = false;
        gateOpen = false;
		
	}
	
	// Update is called once per frame
	void Update () {
        if (enemy.GetComponent<EnemyController>().health <= 0 && ressing == false)
        {
            Invoke("RessEnemy", spawnTimer);
            Instantiate(blood, enemy.transform);
            Debug.Log("777");
            ressing = true;
        }
        else if(enemy.GetComponent<EnemyController>().health >= 0)
        {
            ressing = false;
        }
        OpenGate();
     
		
	}

    void OpenGate()
    {
        if(gateOpen == false)
        {

            foreach (GameObject g in Podiums)
            {
                if (g.GetComponent<BloodPodium>().filled == false)
                {
                    return;
                }
            }
            gate.GetComponent<Gate>().OpenGate();
            gateOpen = true;
        }
       
    }

    void RessEnemy()
    {
        enemy.GetComponent<EnemyController>().Revive();
    }
}

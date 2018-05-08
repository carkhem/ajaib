using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPodiumEnemy : MonoBehaviour {

    public GameObject podiumCollider;
    public GameObject host;
    public bool ressing;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (host.GetComponent<EnemyController>().health == 0)
            Invoke("host.GetComponent<EnemyController>().Revive()", 10);
		//if(GetComponentInParent<PlayerStats>)
	}
}

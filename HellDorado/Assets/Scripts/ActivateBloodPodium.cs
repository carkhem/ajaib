using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBloodPodium : MonoBehaviour {
    public GameObject host;
    private bool ressing;
	// Use this for initialization
	void Start () {
        ressing = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (host.GetComponent<EnemyController>().health <= 0)
        {
            this.gameObject.SetActive(true);
            if (ressing == false)
            {
                Invoke("host.GetComponent<EnemyController>().Revive()", 10);
                ressing = true;
            }
        }
        else
        {
            this.gameObject.SetActive(false);
            ressing = false;
        }
		
	}

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<BloodPodium>().filled = true;
        //this.gameObject.SetActive(false);
    }
}

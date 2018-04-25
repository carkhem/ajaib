using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    public int health;
    public int damage = 10;
	// Use this for initialization
	void Start () {
        health = 30;
		
	}
	
	// Update is called once per frame
	void Update () {


        if (health <= 0)
            this.gameObject.SetActive(false);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<PlayerScript>().changeHealth(-35);
    }
}

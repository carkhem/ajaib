using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {
    public int health;
    public Transform healthBar;
    public Slider healthFill;
    public int maxHealth;
    public GameObject deadScreen;

    
    void Start () {
        //health = 100;
	}
	
	
	void Update () {
		if(health <= 0)
        {
            deadScreen.SetActive(true);
        }
	}
    public void changeHealth(int ammount)
    {
        health += ammount;
        health = Mathf.Clamp(health, 0, maxHealth);

        healthFill.value = health;
    }

    
}

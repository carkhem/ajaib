using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public Text healthProcent;
    public float health;
    public Transform healthBar;
    public Slider healthFill;
    public int maxHealth;
    public GameObject deadScreen;
    public bool inCombat;
    private float timeSecond = 0.0f;
    public int regenerate;


    void Start()
    {
        health = maxHealth;
        healthProcent.text = health + "%";
    }


    void Update()
    {

        if (!GetComponent<AbilityManager>().isRewinding)
            regenerateHealth(regenerate);
        //		if (!inCombat) {
        //			regenerateHealth();
        //		}

        if (health <= 0)
        {
            deadScreen.SetActive(true);
        }
    }
    public void changeHealth(int ammount)
    {
        health += ammount;
        health = Mathf.Clamp(health, 0, maxHealth);

        healthFill.value = health;
        healthProcent.text = health + "%";
    }

    private void regenerateHealth(int regen)
    {

        timeSecond += Time.deltaTime;
        if (timeSecond >= 1)
        {
            changeHealth(regen);
            timeSecond -= (int)timeSecond;
        }

    }

    public void damagePlayer()
    {
        timeSecond += Time.deltaTime;
        Debug.Log(timeSecond);

        if (timeSecond >= 0.05f)
        {

            changeHealth(-1);

            timeSecond -= timeSecond;
        }
        
       
    }

}

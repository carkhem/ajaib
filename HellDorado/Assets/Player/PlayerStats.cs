﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour{
	public static PlayerStats instance;
    private float timeInSec;
    private int LevelUpTextSec;
    private bool LevelIsUp = false;

    [Header("PlayerHealth")]
	private Text healthProcent; //--- tog bara bort för jag vet inte var det passar in. Ta tillbaka om du vill
    public float health;
    public int maxHealth;
    public bool inCombat;
    public float regenSpeed = 2f;
    private Slider healthSlider;

    [Header("Combat")]
	public float meleeDamage;
	private float damage;
	private float sneakDamage;
    public bool sneaking = false;

    [Header("PlayerLevel")]
    public float LevelMaxExp;
    public float playerExp;
    public int PlayerLevel;
    private Slider experienceSlider;
    private Text experienceProgress;

//    private float timeSecond = 0.0f;
//    public int regenerate;

	void Awake(){
		instance = this;
	}

    void Start(){
		healthSlider = CanvasManager.instance.healthSlider;
        health = maxHealth;
		healthProcent = CanvasManager.instance.healthProcent;
        healthProcent.text = health + "%";
        experienceSlider = CanvasManager.instance.experienceSlider;
        experienceProgress = CanvasManager.instance.experienceProgress;
        PlayerLevel = 1;
        playerExp = 0;
    }


    void Update()
    {
        
        healthSlider.value = Mathf.Lerp(healthSlider.value, (health / maxHealth), Time.deltaTime * regenSpeed);
		if (!GetComponent<AbilityManager> ().isRewinding)
			RegenerateHealth ();
		if (health == 0)
		{
			GameManager.instance.GameOver();
		}

        //        if (!GetComponent<AbilityManager>().isRewinding)
        //            regenerateHealth(regenerate);
        //		if (!inCombat) {
        //			regenerateHealth();
        //		}
        if (sneaking)
            damage = sneakDamage;
        
        if (!sneaking)
            damage = meleeDamage;

		healthProcent.text = (int)(health/maxHealth * 100) + "%";
        changeDmg(PlayerLevel);
        LevelMaxExp = PlayerLevel * 100;
        updateExperienceProgress(false);
//        Debug.Log("Player Level är " + PlayerLevel + " Player EXP är " + playerExp + " Player Max Exp för Level är " + LevelMaxExp + "player Damage är " + damage);
    }

	public void ChangeHealth(float ammount){
        
		if (health + ammount > maxHealth)
			health = maxHealth;
		else if (health + ammount < 0)
			health = 0;
		else 
			health += ammount;
		
//        health = Mathf.Clamp(health, 0, maxHealth);

    }

    private void RegenerateHealth() {
		if (health < maxHealth)
			health += Time.deltaTime * regenSpeed;
		else if (health > maxHealth)
			health = maxHealth;
    }

	public void DamagePlayer(float damage) {
		if (health - (damage * Time.deltaTime) > 0) {
			print ("Killing");
			health -= (damage * Time.deltaTime);
		} else if (health - (damage * Time.deltaTime) < 0) {
			print (health + " - " + damage + " = DIE");
			health = 0;
		}
    }

    public void changeDmg(int level){
        meleeDamage = level * 2;
        sneakDamage = meleeDamage * 2;
    }

    public void updateExperienceProgress(bool LevelUp)
    {
        experienceSlider.value = playerExp;
        experienceSlider.maxValue = LevelMaxExp;
        experienceProgress.text = playerExp + "/" + LevelMaxExp;
        if (LevelUp)
        {
            experienceSlider.minValue = (PlayerLevel - 1) * 100;
            LevelIsUp = true;
        }
        LevelUpText();
    }

    public void LevelUpText()
    {
        if (LevelIsUp)
        {
            timeInSec += Time.fixedDeltaTime;
            if (timeInSec > 1)
            {
                LevelUpTextSec += (int)timeInSec;
                timeInSec -= (int)timeInSec;
                if (LevelUpTextSec >= 3)
                {
                    LevelIsUp = false;
                    LevelUpTextSec = 0;
                    timeInSec = 0;
                }
            }
//            CanvasManager.instance.LevelUpPicture.SetActive(LevelIsUp);
        }
    }
}

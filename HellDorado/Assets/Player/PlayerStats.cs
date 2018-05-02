using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour{

    [Header("PlayerHealth")]
    public Text healthProcent; //--- tog bara bort för jag vet inte var det passar in. Ta tillbaka om du vill
    public float health;
    public int maxHealth;
    public bool inCombat;
    public float regenSpeed = 2f;
    private Slider healthSlider;

    [Header("Combat")]
    public int meleeDamage;
    private int damage;
    private int sneakDamage;
    public bool sneaking = false;

    public float LevelMaxExp;
    public float playerExp;
    public int PlayerLevel;
    //    private float timeSecond = 0.0f;
    //    public int regenerate;


    void Start(){
		healthSlider = CanvasManager.instance.healthSlider;
        health = maxHealth;
        healthProcent.text = health + "%";
        PlayerLevel = 1;
        playerExp = 0;
    }


    void Update()
    {
        
        healthSlider.value = Mathf.Lerp(healthSlider.value, (health / maxHealth), Time.deltaTime * regenSpeed);
		if (!GetComponent<AbilityManager> ().isRewinding)
			RegenerateHealth ();
		if (health <= 10)
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

        healthProcent.text = (int)health + "%";
        changeDmg(PlayerLevel);
        LevelMaxExp = PlayerLevel * 100;
        Debug.Log("Player Level är " + PlayerLevel + " Player EXP är " + playerExp + " Player Max Exp för Level är " + LevelMaxExp + "player Damage är " + damage);
    }
    public void ChangeHealth(int ammount)
    {
        health += ammount;
        health = Mathf.Clamp(health, 0, maxHealth);

//		healthSlider.value = health / maxHealth;
       
    }

    private void RegenerateHealth()
    {
		if (health < maxHealth)
			health += Time.deltaTime * regenSpeed;
		else if (health > maxHealth)
			health = maxHealth;
		
//        timeSecond += Time.deltaTime;
//        if (timeSecond >= 1)
//        {
//            changeHealth(regen);
//            timeSecond -= (int)timeSecond;
//        }
    }

	public void DamagePlayer(float damage)
    {
//        timeSecond += Time.deltaTime;
//        Debug.Log(timeSecond);
//
//        if (timeSecond >= 0.05f)
//        {
//
//            changeHealth(-1);
//
//            timeSecond -= timeSecond;
//        }

		if (health > 0)
			health -= damage * Time.deltaTime;
		else if (health < 0) {
			health = 0;
		}
       
    }

    public void changeDmg( int level)
    {
        meleeDamage = level * 2;
        sneakDamage = meleeDamage * 2;
    }

}

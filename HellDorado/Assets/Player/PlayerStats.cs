using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour{
    public Text healthProcent; //--- tog bara bort för jag vet inte var det passar in. Ta tillbaka om du vill
    public float health;
	
    public int maxHealth;
    public bool inCombat;
    public bool sneaking = false;
//    private float timeSecond = 0.0f;
//    public int regenerate;
	public float regenSpeed = 2f;
    private Slider healthSlider;

    void Start(){
		healthSlider = CanvasManager.instance.healthSlider;
        health = maxHealth;
        healthProcent.text = health + "%";
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

        healthProcent.text = (int)health + "%";
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

}

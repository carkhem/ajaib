using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {
    public GameObject fireballPrefab;
    public Transform fireballSpawn;
    public GameObject player;
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
            Fire();
        
        
    }

    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        var fireball = (GameObject)Instantiate(
            fireballPrefab,
            fireballSpawn.position,
            fireballSpawn.rotation);

        fireball.GetComponent<Rigidbody>().velocity = fireball.transform.forward * 8;


        player.GetComponent<PlayerScript>().changeHealth(-20);
        // Destroy the bullet after 2 seconds
        Destroy(fireball, 2.0f);
        // Add velocity to the bullet

    }

   
}

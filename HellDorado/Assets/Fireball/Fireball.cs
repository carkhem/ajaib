using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {
    public GameObject fireballPrefab;
    public Transform ballSpawn;
    public float speed = 6f;
    public float lifeTime = 2f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire();
        }
    }

    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        var fireball = (GameObject)Instantiate(
            fireballPrefab,
            ballSpawn.position,
            ballSpawn.rotation);

        // Add velocity to the bullet
        fireball.GetComponent<Rigidbody>().velocity = fireball.transform.forward * speed;

        // Destroy the bullet after 2 seconds
        Destroy(fireball, lifeTime);
    }
}

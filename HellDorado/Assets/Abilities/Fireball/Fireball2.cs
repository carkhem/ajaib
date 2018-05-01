using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball2 : MonoBehaviour {

	public float lifetime = 2;
	public float speed = 2;
	public float maxDamage = 2;
	public float minDamage = 1;
	public GameObject explosionPrefab;

	private float damage;
	private float timer;

	void Start () {
		damage = maxDamage; //Behöver kanske inte den här raden.
		timer = 0;
	}
	
	void Update () {
		//Åk framåt i den hastighet vi vill.
		//transform.Translate (transform.forward * speed * Time.deltaTime);
		GetComponent<Rigidbody>().velocity = transform.forward * speed;

		//Minska damage över tid
		timer += Time.deltaTime;
		damage = Mathf.Lerp (maxDamage, minDamage, timer / lifetime);

		//Förstör när livstiden är nådd.
		if (timer >= lifetime) {
			Explode ();
		}
	}

	private void Explode(){
		if (explosionPrefab != null)
			//Instantiate (explosionPrefab, transform.position, transform.rotation);
		//explosion.particleSystem.particleEmitter -- Sätt storleken på explosionen på nåt sätt.
		GameObject.Destroy (gameObject);
        GameObject.Destroy(Instantiate(explosionPrefab, transform.position, transform.rotation), 1.5f);
	}

	void OnCollisionEnter(Collision col){
        if(!col.gameObject.CompareTag("Player") && !col.gameObject.CompareTag("MainCamera")) {
            Explode();
        } 
	}

	public float GetDamage(){
		return damage;
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball2 : MonoBehaviour {

	public float lifetime = 2;
	public float speed = 2;
	public float maxDamage = 2;
	public float minDamage = 1;
	public GameObject explosionPrefab;
	public AudioClip explosion;
	public AudioSource source;

	private Vector3 endPos;

	private float damage;
	private float timer;

	void Start () {
		damage = maxDamage; //Behöver kanske inte den här raden.
		timer = 0;

		endPos = Aim();
		source = GetComponent<AudioSource> ();
	}
	
	void Update () {
		//Åk framåt i den hastighet vi vill.
		GetComponent<Rigidbody>().velocity = transform.forward * speed;

		//Minska damage över tid
		timer += Time.deltaTime;
		damage = Mathf.Lerp (maxDamage, minDamage, timer / lifetime);

		//Sluta emitta när livstiden är nådd.
		if (timer >= lifetime) {
			GetComponent<SphereCollider> ().enabled = false;
			StopEmitting();
		}
		DestroyIfDead ();
	}

	private void DestroyIfDead(){
		for ( int i = 0; i < transform.childCount; i++){
			if (transform.GetChild (i).GetComponent<ParticleSystem> ().IsAlive()) {
				return;
			}
		}
		if (!source.isPlaying)
			GameObject.Destroy (gameObject);
	}

	private void StopEmitting(){
		for ( int i = 0; i < transform.childCount; i++){
			transform.GetChild (i).GetComponent<ParticleSystem> ().Stop ();
		}
	}

	private void Explode(){
		if (explosionPrefab != null)
			StopEmitting();
			source.PlayOneShot(explosion);
        	Instantiate (explosionPrefab, transform.position, transform.rotation);
	}

	public void SetMaxDamage(float damage){
		maxDamage = damage;
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.CompareTag("Enemy")) {
			col.gameObject.GetComponent<EnemyController> ().TakeDamage (damage);
		}
		if(!col.gameObject.CompareTag("Player") && !col.gameObject.CompareTag("MainCamera") && !col.gameObject.CompareTag("Sword")) {
            Explode();
        }
		if (col.gameObject.CompareTag ("Player")) {
			col.gameObject.GetComponent<PlayerStats> ().ChangeHealth (-20f);
			Explode ();
		}

	}

	public float GetDamage(){
		return damage;
	}

	public Vector3 Aim(){
		Ray ray = Camera.main.ScreenPointToRay(new Vector3 (Screen.width / 2, Screen.height / 2, 0));
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit)) {
			endPos = ray.GetPoint (200);
			return endPos;
		} else {
			endPos = ray.GetPoint(200);
			return endPos;
		}
	}
}

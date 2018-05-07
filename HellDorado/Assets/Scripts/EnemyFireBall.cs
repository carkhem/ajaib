using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireBall : MonoBehaviour {

	public GameObject player;
	public GameObject fireBallPrefab;
	public Transform fireBallSpawn;

	public float shootDelay = 5f;
	private float originalDelay;
	public float fireBallDamage;
	public float lifetime = 7f;

	// Use this for initialization
	void Start () {
		originalDelay = shootDelay;
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (player.transform);

		shootDelay -= Time.deltaTime;
		if (shootDelay <= 0f) {
			shootDelay = originalDelay;
			ShootFireBall ();
		}
	}


	private void ShootFireBall(){
		GameObject ball = Instantiate(fireBallPrefab, fireBallSpawn.position, fireBallSpawn.rotation);
		ball.GetComponent<Fireball2> ().SetMaxDamage (fireBallDamage);
		ball.GetComponent<Fireball2> ().lifetime = lifetime;
	}
}

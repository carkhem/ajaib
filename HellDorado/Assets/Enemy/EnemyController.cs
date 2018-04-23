using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Controller {
	[Header("Movement")]
	public float runSpeed;
	public float walkSpeed;
	[Header("Stats")]
	public float damage;
	public float health;
	[Header("Sight")]
	public float fov;
	public float viewDistance;
	public float detectionTime;
	public Transform player;
	private Vector3 playerDirection;

	void Update(){
		if (health <= 0) {
			TransitionTo<DeadState> ();
		}

		playerDirection = player.position - transform.position;
		RaycastHit hit;
		if(Physics.Raycast(transform.position, playerDirection, out hit, viewDistance)){
			if (hit.transform.CompareTag("Player")){
				if (Vector3.Angle (transform.forward, playerDirection) < fov) {
					//Player in field of view
					Debug.DrawRay (transform.position, playerDirection, Color.green);
				} else {
					Debug.DrawRay (transform.position, playerDirection, Color.red);
				}
			}
		}
		Debug.DrawRay (transform.position, Quaternion.AngleAxis(fov, transform.up) * transform.forward * viewDistance);
		Debug.DrawRay (transform.position, Quaternion.AngleAxis(-fov, transform.up) * transform.forward * viewDistance);
	}
}

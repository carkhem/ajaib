using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : Controller {
//	[Header("Movement")]
//	public float runSpeed;
//	public float walkSpeed;
//	[HideInInspector]
//	public NavMeshAgent agent;
	[Header("Stats")]
	public float damage;
	public float health;
	[Header("Sight")]
	public float fov;
	public float viewDistance;
	public float detectionSpeed;
	private float detection; //Kan användas för att visa på hur nära man är på att bli upptäckt
	private float detectionTimer = 0;
	public Transform player;
	private Vector3 playerDirection;

//	void Awake(){
//		//Sätt player direkt! Typ: player = PlayerController.instance.transform;
		//EDIT: det går inte att använda awake när man använder den här state machinen. Gör det i någons Initialize-funktion.
//	}

//	void Awake(){
//		agent = transform.GetComponent<NavMeshAgent> ();
//	}

	public void CheckHealth(){
		if (health <= 0) {
			TransitionTo<DeadState> ();
		}
	}

	public void Look(){
		playerDirection = player.position - transform.position;
		RaycastHit hit;
		if (Physics.Raycast (transform.position, playerDirection, out hit, viewDistance)) {
			if (hit.transform.CompareTag ("Player")) {
				if (Vector3.Angle (transform.forward, playerDirection) < fov) {
					if (detectionTimer < detectionSpeed)
						detectionTimer += Time.deltaTime / Vector3.Distance(transform.position, player.position) * 8;
					else
						detectionTimer = detectionSpeed;
					Debug.DrawRay (transform.position, playerDirection, Color.green);
				} else {
					if (detectionTimer > 0)
						detectionTimer -= Time.deltaTime;
					else
						detectionTimer = 0;
					Debug.DrawRay (transform.position, playerDirection, Color.red);
				}
			}
		} else {
			if (detectionTimer > 0)
				detectionTimer -= Time.deltaTime;
			else
				detectionTimer = 0;
		}
		detection = detectionTimer / detectionSpeed;
		if (detection >= 1) {
			detection = 1;
			TransitionTo<CombatState> ();
		}
		
		Debug.DrawRay (transform.position, Quaternion.AngleAxis(fov, transform.up) * transform.forward * viewDistance);
		Debug.DrawRay (transform.position, Quaternion.AngleAxis(-fov, transform.up) * transform.forward * viewDistance);
	}

	//DEN HÄR FUNKAR TYP INTE MED NEGATIVA NUMMER! JAG FATTAR INGENTING!--------------------------------------------
	public bool SamePosition(Vector3 posOne, Vector3 posTwo){
//		print ((int)posOne.x + " = " + (int)posTwo.x + " | " + (int)posOne.z + " = " + (int)posTwo.z);
		if ((int)posOne.x == (int)posTwo.x && (int)posOne.z == (int)posTwo.z) {
			return true;
		} else {
			return false;
		}
	}

}

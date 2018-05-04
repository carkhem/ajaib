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
	public GameObject eyes;
	public Vector3 offset;
	public float fov;
	public float viewDistance;
	public float detectionSpeed;
	public float detection; //Kan användas för att visa på hur nära man är på att bli upptäckt
	private float detectionTimer = 0;
	[HideInInspector]
	public Transform player;
	[Header("Movement")]
	public Animator anim;

//	void Awake(){
//		//Sätt player direkt! Typ: player = PlayerController.instance.transform;
		//EDIT: det går inte att använda awake när man använder den här state machinen. Gör det i någons Initialize-funktion.
//	}

	void Start(){
		player = PlayerStats.instance.transform;
	}

	public void CheckHealth(){
		if (health <= 0) {
			TransitionTo<DeadState> ();
		}
	}

	public bool InSight(Transform thing){
		Vector3 direction = thing.position - eyes.transform.position;
		RaycastHit hit;
		if (Physics.Raycast (eyes.transform.position, direction, out hit, viewDistance)) {
			if (hit.transform.CompareTag (thing.tag)) {
				if (Vector3.Angle (eyes.transform.forward, direction) < fov) {
					Debug.DrawRay (eyes.transform.position, direction, Color.green);
					return true;
				} else {
					Debug.DrawRay (eyes.transform.position, direction, Color.red);
					return false;
				}
			} else {
				return false;
			}
		} else {
			return false;
		}
	}

	public void TakeDamage(float damage){
		health -= damage;
		Debug.Log (health);
		if (health > 0)
			TransitionTo<StunnedState> ();
		else
			TransitionTo<DeadState> ();
	}

	public void LookForPlayer(){
		Debug.DrawRay (eyes.transform.position, Quaternion.AngleAxis(fov, eyes.transform.up) * eyes.transform.forward * viewDistance);
		Debug.DrawRay (eyes.transform.position, Quaternion.AngleAxis(-fov, eyes.transform.up) * eyes.transform.forward * viewDistance);

//		detectionMultiplier = Vector3.Distance (transform.position, player.position) * 8;
//		detectionMultiplier = 5 / Vector3.Angle (transform.forward, player.position - transform.position);

		if (InSight(player)) {
			if (detectionTimer < detectionSpeed)
				detectionTimer += Time.deltaTime / Vector3.Distance (eyes.transform.position, player.position) * 8;
			else
				detectionTimer = detectionSpeed;
		} else {
			if (detectionTimer > 0)
				detectionTimer -= Time.deltaTime;
			else
				detectionTimer = 0;
			}
		detection = detectionTimer / detectionSpeed;
		if (detection >= 1) {
			detection = 1;
			detectionTimer = 0;
			TransitionTo<CombatState> ();
		}
	}

	//DEN HÄR FUNKAR INTE ALLTID AV NÅGON ANLEDNING
	public bool SamePosition(Vector3 posOne, Vector3 posTwo){
		if ((int)posOne.x == (int)posTwo.x && (int)posOne.z == (int)posTwo.z) {
			return true;
		} else {
			return false;
		}
	}

	public bool IsMoving(NavMeshAgent agent){
		if (agent.velocity.x == 0 && agent.velocity.z == 0)
			return false;
		else
			return true;
	}

	public void SetAnim(string boolName, bool condition){
		anim.SetBool (boolName, condition);
	}

}

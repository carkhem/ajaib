using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Enemy/States/Attack")]
public class AttackState : State {
	public float hitRange;
	public float plungeSpeed = 5;
	public float recoverTime = 2;
	private float timer = 0;
	private Vector3 attackPos;

	private EnemyController _controller;
	private NavMeshAgent agent;

	public override void Initialize(Controller owner) {
		_controller = (EnemyController)owner;
		agent = transform.GetComponent<NavMeshAgent> ();
	}

	public override void Enter (){
		Debug.Log ("ATTACK!");
		attackPos = _controller.player.position;
		agent.speed = plungeSpeed;
		timer = 0;
	}

	public override void Update (){
		Debug.Log (agent.velocity);
		agent.SetDestination (attackPos);
		if (agent.velocity.x == 0 && agent.velocity.z == 0) {
			timer += Time.deltaTime;
			if (timer >= recoverTime) {
				Debug.Log ("Recovered");
				_controller.TransitionTo<CombatState> ();
			}
		}
	}
}

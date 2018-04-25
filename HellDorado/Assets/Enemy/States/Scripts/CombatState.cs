﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Enemy/States/Combat")]
public class CombatState : State {
	[Header("Movement")]
	public float runSpeed;
	private NavMeshAgent agent;
	public float stopDistance = 1f;
	private Vector3 lastKnownPos;
	public float logicFollowTime = 2f;
	private float logicFollowTimer = 0;
	[Header("Fighting")]
	public MinMaxFloat attackWait;
	private float currentAttackWait;
	private float attackTimer = 0;

	private EnemyController _controller;


	public override void Initialize(Controller owner) {
		_controller = (EnemyController)owner;
		agent = transform.GetComponent<NavMeshAgent> ();
	}

	public override void Enter (){
		Debug.Log (transform.name + ": " + _controller.CurrentState.name);
		transform.GetComponent<NavMeshAgent> ().enabled = true;
		attackTimer = 0;
		currentAttackWait = Random.Range (attackWait.Min, attackWait.Max);
		agent.speed = runSpeed;
		logicFollowTimer = 0;
	}

	public override void Update (){
		//Stannar för skarpt. Vill egentligen hitta en vector mellan tarnsform och player som är stopDistance ifrån playern.
		if (_controller.InSight (_controller.player)) {
			lastKnownPos = _controller.player.position;
		}

		if (!_controller.IsMoving(agent) && !_controller.InSight(_controller.player)) {
			_controller.TransitionTo<SearchingState> ();
		}

		if (Vector3.Distance (transform.position, _controller.player.position) > stopDistance) {
			if (_controller.InSight (_controller.player)) {
				logicFollowTimer = 0;
				agent.SetDestination (_controller.player.position);
			} else {
				logicFollowTimer += Time.deltaTime;
				if (logicFollowTimer >= logicFollowTime) {
					agent.SetDestination (lastKnownPos);
				} else {
					agent.SetDestination (_controller.player.position);
				}
			}
		} else {
			agent.SetDestination (transform.position);
			attackTimer += Time.deltaTime;
			if (attackTimer >= currentAttackWait) {
				_controller.TransitionTo<AttackState> ();
			}
		}

		//Vill ha en mer smooth LookAt
		transform.LookAt (new Vector3 (_controller.player.position.x, transform.position.y, _controller.player.position.z));


	}
}

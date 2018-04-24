using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Enemy/States/Patrol")]
public class PatrolState : State {
	public float walkSpeed;
	private NavMeshAgent agent;
	public Vector3[] patrolPoints;
	private Vector3 destination;

	private EnemyController _controller;

	public override void Initialize(Controller owner) {
		_controller = (EnemyController)owner;
		agent = transform.GetComponent<NavMeshAgent> ();
	}

	public override void Enter() {
		RandomizeDestination ();
		transform.GetComponent<NavMeshAgent> ().enabled = true;
		agent.speed = walkSpeed;
	}

	public override void Update (){
		agent.SetDestination (destination);
		if (_controller.SamePosition (destination, transform.position)) {
			_controller.TransitionTo<IdleState> ();
		}
		_controller.Look ();
		_controller.CheckHealth ();
	}

	public override void Exit (){
		agent.SetDestination (transform.position);
	}

	private void RandomizeDestination(){
		do {
			destination = patrolPoints[Random.Range(0, patrolPoints.Length)];
			} while (_controller.SamePosition(destination, transform.position));
	}
}

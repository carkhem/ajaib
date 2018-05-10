using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Enemy/States/Patrol")]
public class PatrolState : State {
	public float walkSpeed;
	private NavMeshAgent agent;
//	public Vector3[] patrolPoints;
	private Vector3 destination;

	private EnemyController _controller;

	public override void Initialize(Controller owner) {
		_controller = (EnemyController)owner;
		agent = transform.GetComponent<NavMeshAgent> ();
	}

	public override void Enter() {
		_controller.SetAnim ("walk", true);
		RandomizeDestination ();
		transform.GetComponent<NavMeshAgent> ().enabled = true;
		agent.speed = walkSpeed;
		//Debug.Log (transform.name + ": " + _controller.CurrentState.name + " | " + destination);
	}

	public override void Update (){
		agent.SetDestination (destination);
		if (_controller.SamePosition (destination, transform.position)) {
			_controller.TransitionTo<IdleState> ();
		}
		_controller.LookForPlayer();
	}

	public override void Exit (){
		agent.SetDestination (transform.position);
		_controller.SetAnim ("walk", false);
	}

	private void RandomizeDestination(){
		do {
			destination = _controller.patrolPoints[Random.Range(0, _controller.patrolPoints.Length)];
			} while (_controller.SamePosition(destination, transform.position));
	}
}

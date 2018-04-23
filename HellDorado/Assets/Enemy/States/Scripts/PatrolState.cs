using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Enemy/States/Patrol")]
public class PatrolState : State {
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
	}

	public override void Update (){
		agent.SetDestination (destination);
		if (SamePosition (destination, transform.position)) {
			_controller.TransitionTo<IdleState> ();
//			RandomizeDestination();
		}
	}

	private void RandomizeDestination(){
		do {
			destination = patrolPoints[Random.Range(0, patrolPoints.Length)];
			} while (SamePosition(destination, transform.position));
	}

	private bool SamePosition(Vector3 posOne, Vector3 posTwo){
		if (posOne.x == posTwo.x && (int)posOne.z == (int)posTwo.z) {
			return true;
		} else {
			return false;
		}
	}
}

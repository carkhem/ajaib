using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Enemy/States/Dead")]
public class DeadState : State {
	private EnemyController _controller;


	public override void Initialize(Controller owner) {
		_controller = (EnemyController)owner;
	}

	public override void Enter (){
		//Play dead-animation OR use a ragdoll :D
		_controller.SetAnim("die", true);
//		Debug.Log (transform.name + ": " + _controller.CurrentState.name);
//		transform.eulerAngles = new Vector3 (90, transform.eulerAngles.y, transform.eulerAngles.z);
		transform.GetComponent<NavMeshAgent> ().enabled = false;
		transform.GetComponent<BoxCollider> ().enabled = false;
		_controller.player.GetComponent<PlayerStats> ().RemoveEnemy (transform.gameObject);
	}

	public override void Update (){
		//PROBLEM IBLAND
		if (_controller.health > 0)
			_controller.TransitionTo<PatrolState> ();
	}
}

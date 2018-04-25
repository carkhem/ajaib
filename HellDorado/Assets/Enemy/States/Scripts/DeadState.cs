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
		Debug.Log ("DED");
		transform.eulerAngles = new Vector3 (90, transform.eulerAngles.y, transform.eulerAngles.z);
		transform.GetComponent<NavMeshAgent> ().enabled = false;
	}

	public override void Update (){
		if (_controller.health > 0)
			_controller.TransitionTo<PatrolState> ();
	}
}

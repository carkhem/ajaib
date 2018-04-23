using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Enemy/States/Combat")]
public class CombatState : State {
	public float hitRange;

	private EnemyController _controller;


	public override void Initialize(Controller owner) {
		_controller = (EnemyController)owner;
	}

	public override void Enter (){
		transform.GetComponent<NavMeshAgent> ().enabled = true;
	}

	public override void Update (){

	}
}

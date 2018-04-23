using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/States/Idle")]
public class IdleState : State {
	public MinMaxFloat idleTime;
	private float currentIdleTime;
	private float timer;

	private EnemyController _controller;

	public override void Initialize (Controller owner){
		_controller = (EnemyController)owner;
	}

	public override void Enter (){
		timer = 0;
		currentIdleTime = Random.Range (idleTime.Min, idleTime.Max);
	}

	public override void Update (){
		timer += Time.deltaTime;
		if (timer > currentIdleTime) {
			_controller.TransitionTo<PatrolState>();
		}
		_controller.Look ();
		_controller.CheckHealth ();
	}

}

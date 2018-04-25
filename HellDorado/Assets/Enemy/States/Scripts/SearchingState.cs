using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/States/Attack")]
public class SearchingState : State {
	public float searchingTime;
	private float timer = 0;

	private EnemyController _controller;

	public override void Initialize (Controller owner){
		_controller = (EnemyController)owner;
	}

	public override void Enter (){
		timer = 0;
	}

	public override void Update (){
		timer += Time.deltaTime;
		if (timer >= searchingTime) {
			_controller.TransitionTo<PatrolState> ();
		}
	}
}

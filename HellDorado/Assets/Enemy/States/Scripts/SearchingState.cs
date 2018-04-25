using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/States/Search")]
public class SearchingState : State {
	public float searchingTime;
	private float timer = 0;

	private EnemyController _controller;

	public override void Initialize (Controller owner){
		_controller = (EnemyController)owner;
	}

	public override void Enter (){
		Debug.Log (transform.name + ": " + _controller.CurrentState.name);
		timer = searchingTime;
	}

	public override void Update (){
		timer -= Time.deltaTime;
		_controller.detection = timer / searchingTime;
		if (timer <= 0) {
			_controller.detection = 0;
			_controller.TransitionTo<PatrolState> ();
		}
	}
}

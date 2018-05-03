using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/States/Stunned")]
public class StunnedState : State {

	private float timer;
	public float stunnedTime;
	private EnemyController _controller;

	public override void Initialize(Controller owner) {
		_controller = (EnemyController)owner;
	}

	public override void Enter (){
		Debug.Log ("STUNNED");
	}

	public override void Update(){
		timer += Time.deltaTime;
		if (timer >= stunnedTime) {
			_controller.TransitionTo<CombatState> ();
		}
	}

}

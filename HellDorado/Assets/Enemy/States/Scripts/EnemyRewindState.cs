using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/States/EnemyRewindState")]
public class EnemyRewindState : State {

	private EnemyController _controller;
	private TimeBody timeBody;

	public override void Initialize (Controller owner){
		_controller = (EnemyController)owner;
		timeBody = transform.GetComponent<TimeBody> ();
	}

	public override void Enter(){
		timeBody.StartRewind ();
	}
		

	public override void Update (){
		if (!AbilityManager.WorldRewind || !timeBody.isRewinding) {
			timeBody.StopRewind ();
			_controller.TransitionTo<IdleState> ();
		}
			
	}
}

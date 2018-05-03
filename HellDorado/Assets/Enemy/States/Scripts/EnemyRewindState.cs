using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/States/EnemyRewindState")]
public class EnemyRewindState : State {

	private EnemyController _controller;

	public override void Initialize (Controller owner){
		_controller = (EnemyController)owner;
	}


		

	public override void Update (){
		if (!TimeBody.isRewinding) {

			//Vill gå tillbaka till föregående state här istället för alltid Idle
			_controller.TransitionTo<IdleState> ();
		}
			
	}
}

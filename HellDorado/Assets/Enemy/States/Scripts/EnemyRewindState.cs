using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/States/EnemyRewindState")]
public class EnemyRewindState : State {

	private EnemyController _controller;

	public override void Initialize (Controller owner){
		_controller = (EnemyController)owner;
	}


	public override void Enter(){

	}

	public override void Update (){


		//Temporärt. Får kolla hur mycket som behöver läggas på under rewind
		_controller.health += 0.1f;
		if (!TimeBody.isRewinding) {

			RecentState ();
		}
			
	}

	private void RecentState(){
		switch (_controller.recentState) {
		case "AttackState":
			_controller.TransitionTo<AttackState>();
			break;
		case "CombatState":
			_controller.TransitionTo<CombatState>();
			break;
		case "DeadState":
			_controller.TransitionTo<DeadState>();
			break;
		case "IdleState":
			_controller.TransitionTo<IdleState>();
			break;
		case "PatrolStat":
			_controller.TransitionTo<PatrolState>();
			break;
		case "SearchingState":
			_controller.TransitionTo<SearchingState>();
			break;
		case "StunnedState":
			_controller.TransitionTo<StunnedState>();
			break;
			default:
			_controller.TransitionTo<PatrolState>();
			break;
		}
	}

}

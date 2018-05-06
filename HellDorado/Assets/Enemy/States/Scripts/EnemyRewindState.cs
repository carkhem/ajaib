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
			Debug.Log ("1");
			_controller.TransitionTo<AttackState>();
			break;
		case "CombatState":
			Debug.Log ("2");
			_controller.TransitionTo<CombatState>();
			break;
		case "DeadState":
			Debug.Log ("3");
			_controller.TransitionTo<DeadState>();
			break;
		case "IdleState":
			Debug.Log ("4");
			_controller.TransitionTo<IdleState>();
			break;
		case "PatrolStat":
			Debug.Log ("5");
			_controller.TransitionTo<PatrolState> ();
			break;
		case "SearchingState":
			Debug.Log ("6");
			_controller.TransitionTo<SearchingState>();
			break;
		case "StunnedState":
			Debug.Log ("7");
			_controller.TransitionTo<StunnedState>();
			break;
			default:
			Debug.Log ("8");
			_controller.TransitionTo<PatrolState>();
			break;
		}
	}

}

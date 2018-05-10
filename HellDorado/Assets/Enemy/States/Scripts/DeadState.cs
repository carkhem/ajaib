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
		_controller.SetAnim("die", true);
		_controller.health = 0;
		transform.GetComponent<NavMeshAgent> ().enabled = false;
		transform.GetComponent<BoxCollider> ().enabled = false;
		_controller.player.GetComponent<PlayerStats> ().RemoveEnemy (transform.gameObject);
	}

	public override void Update (){
		//PROBLEM IBLAND
		if (_controller.health > 0)
			_controller.TransitionTo<PatrolState> ();
	}

    public override void Exit()
    {
        _controller.SetAnim("die", false);
        transform.GetComponent<BoxCollider>().enabled = true;
    }
}

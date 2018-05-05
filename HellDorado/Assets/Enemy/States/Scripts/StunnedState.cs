using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Enemy/States/Stunned")]
public class StunnedState : State {

	private float timer;
	public float stunnedTime;
	private EnemyController _controller;
	private NavMeshAgent agent;


	public override void Initialize(Controller owner) {
		_controller = (EnemyController)owner;
		agent = transform.GetComponent<NavMeshAgent> ();
	}

	public override void Enter (){
//		Debug.Log ("STUNNED");
		timer = 0;
		_controller.SetAnim("stunned", true);
		agent.enabled = false;
	}

	public override void Update(){
		timer += Time.deltaTime;
		if (timer >= stunnedTime) {
			_controller.TransitionTo<CombatState> ();
		}
	}

	public override void Exit (){
		_controller.SetAnim("stunned", false);
	}

}

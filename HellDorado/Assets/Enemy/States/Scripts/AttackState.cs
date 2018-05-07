using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Enemy/States/Attack")]
public class AttackState : State {
	public float hitRange;
	public float plungeSpeed = 5;
	public float recoverTime = 2;
	private float timer = 0;
//	private Vector3 attackPos;

	private EnemyController _controller;
	private NavMeshAgent agent;

	public override void Initialize(Controller owner) {
		_controller = (EnemyController)owner;
		agent = transform.GetComponent<NavMeshAgent> ();
	}

	public override void Enter (){
		_controller.SetAnim ("attack", true);
//		Debug.Log (transform.name + ": " + _controller.CurrentState.name);
//		attackPos = _controller.player.position;
		agent.speed = plungeSpeed;
		timer = 0;
	}

	public override void Update (){
//		agent.SetDestination (attackPos);
//		if (!_controller.IsMoving(agent)) {
//			timer += Time.deltaTime;
//			if (timer >= recoverTime) {
////				Debug.Log ("Recovered");
//				_controller.TransitionTo<CombatState> ();
//			}
//		}
		if (_controller.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1){
			_controller.TransitionTo<CombatState> ();
		}
	}

	public override void Exit (){
		_controller.SetAnim ("attack", false);
	}
}

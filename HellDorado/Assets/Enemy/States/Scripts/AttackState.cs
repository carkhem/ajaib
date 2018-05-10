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

	private EnemyController _controller;
	private NavMeshAgent agent;

	public override void Initialize(Controller owner) {
		_controller = (EnemyController)owner;
		agent = transform.GetComponent<NavMeshAgent> ();
	}

	public override void Enter (){
		_controller.SetAnim ("attack", true);
		_controller.GetComponent<EnemySound> ().PlaySwingSound ();
		agent.speed = plungeSpeed;
		timer = 0;
	}

	public override void Update (){
		if (!_controller.anim.GetCurrentAnimatorStateInfo(0).IsName ("Attack")){
			_controller.TransitionTo<CombatState> ();
		}
	}

	public override void Exit (){
		_controller.SetAnim ("attack", false);
	}
}

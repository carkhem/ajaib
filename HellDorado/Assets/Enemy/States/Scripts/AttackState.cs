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
		Debug.Log ("ATTACK");
		_controller.SetAnim ("attack", true);
		_controller.GetComponent<EnemySounds> ().PlaySwingSound ();
		agent.speed = 0;
		timer = 0;
	}

	public override void Update (){
//		if (!_controller.anim.GetCurrentAnimatorStateInfo(0).IsName ("Attack")){
		timer += Time.deltaTime;
		if (timer >= recoverTime){
			Debug.Log ("TRANSITION");
            _controller.GetComponent<EnemySounds>().StopSwingSound();
            _controller.TransitionTo<CombatState> ();
		}
	}

	public override void Exit (){
		_controller.SetAnim ("attack", false);
	}
}
﻿using System.Collections;
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
		_controller.anim.SetTrigger("stun");
		agent.enabled = false;
	}

	public override void Update(){
		timer += Time.deltaTime;
		if (timer >= stunnedTime) {
			_controller.TransitionTo<CombatState> ();
		}

//		Debug.Log(!_controller.anim.GetCurrentAnimatorStateInfo(0).tagHash)
//		if (!_controller.anim.GetCurrentAnimatorStateInfo (0).IsName ("Stunned")){
//			Debug.Log ("transition");
//			_controller.TransitionTo<CombatState> ();
//		}
	}

	public override void Exit (){
		_controller.SetAnim("stunned", false);
	}

}

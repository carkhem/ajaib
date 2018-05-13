using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Enemy/States/FireballAttack")]
public class FireballAttackState : State {

	private EnemyController _controller;
	private NavMeshAgent agent;
	public GameObject fireballPrefab;
	private bool hasAttacked = false;

	public override void Initialize(Controller owner) {
		_controller = (EnemyController)owner;
		agent = transform.GetComponent<NavMeshAgent> ();
	}

	public override void Enter (){
		hasAttacked = false;
		_controller.SetAnim ("attack", true);
		_controller.GetComponent<EnemySound> ().PlaySwingSound ();
		Instantiate (fireballPrefab, transform.position + transform.forward, transform.rotation);
	}

	public override void Update (){
//		if (_controller.anim.GetCurrentAnimatorStateInfo (0).normalizedTime > 0.9f && !hasAttacked) {
//			Debug.Log (_controller.anim.GetCurrentAnimatorStateInfo (0).normalizedTime);
//			Instantiate (fireballPrefab, transform.position + transform.forward, transform.rotation);
//			hasAttacked = true;
//		}
		if (!_controller.anim.GetCurrentAnimatorStateInfo(0).IsName ("Attack")){
			_controller.TransitionTo<CombatState> ();
		}
	}

	public override void Exit (){
		_controller.SetAnim ("attack", false);
	}
}

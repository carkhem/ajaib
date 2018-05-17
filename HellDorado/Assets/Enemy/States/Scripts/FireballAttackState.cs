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
	private float timer;
	public float attackTime = 1.2f;
	public float recoverTime = 0.8f;

	public override void Initialize(Controller owner) {
		_controller = (EnemyController)owner;
		agent = transform.GetComponent<NavMeshAgent> ();
	}

	public override void Enter (){
		timer = 0;
		hasAttacked = false;
		_controller.SetAnim ("attack", true);
		_controller.GetComponent<EnemySound> ().PlaySwingSound ();

	}

	public override void Update (){
//		if (_controller.anim.GetCurrentAnimatorStateInfo (0).normalizedTime > 0.9f && !hasAttacked) {
//			Debug.Log (_controller.anim.GetCurrentAnimatorStateInfo (0).normalizedTime);
//			Instantiate (fireballPrefab, transform.position + transform.forward, transform.rotation);
//			hasAttacked = true;
//		}
		timer += Time.deltaTime;
		if (timer >= recoverTime){
			_controller.TransitionTo<CombatState> ();
		}
		if (timer >= attackTime && !hasAttacked){
			Debug.Log ("Attack");
			hasAttacked = true;
			Instantiate (fireballPrefab, transform.position + transform.forward, transform.rotation);
		}
	}

	public override void Exit (){
		_controller.SetAnim ("attack", false);
	}
}

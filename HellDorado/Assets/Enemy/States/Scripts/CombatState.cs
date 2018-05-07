using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Enemy/States/Combat")]
public class CombatState : State {
	[Header("Movement")]
	public float runSpeed;
	private NavMeshAgent agent;
	public float stopDistance = 1f;
	private Vector3 lastKnownPos;
	public float logicFollowTime = 2f;
	private float logicFollowTimer = 0;
	private float rotTimer = 0;
	[Header("Fighting")]
	public MinMaxFloat attackWait;
	private float currentAttackWait;
	private float attackTimer = 0;

	private EnemyController _controller;


	public override void Initialize(Controller owner) {
		_controller = (EnemyController)owner;
		agent = transform.GetComponent<NavMeshAgent> ();
	}

	public override void Enter (){
		_controller.SetAnim ("activeIdle", true);
		Debug.Log (transform.name + ": " + _controller.CurrentState.name);
		transform.GetComponent<NavMeshAgent> ().enabled = true;
		attackTimer = 0;
		rotTimer = 0;
		currentAttackWait = Random.Range (attackWait.Min, attackWait.Max);
		agent.speed = runSpeed;
		logicFollowTimer = 0;
		lastKnownPos = _controller.player.position;
	}

	public override void Update (){
		//Det första som händer är att den tittar mot spelarens sista position
		if (rotTimer < 1) {
			rotTimer += Time.deltaTime;
		}
		transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation(lastKnownPos - transform.position), Mathf.Abs(rotTimer / 1));

		//Stannar för skarpt. Vill egentligen hitta en vector mellan tarnsform och player som är stopDistance ifrån playern.
		if (_controller.InSight (_controller.player)) {
			lastKnownPos = _controller.player.position;
		}

		if (!_controller.IsMoving(agent) && !_controller.InSight(_controller.player) && rotTimer >= 1) {
			_controller.TransitionTo<SearchingState> ();
		}

		if (Vector3.Distance (transform.position, _controller.player.position) > stopDistance) {
			_controller.SetAnim ("run", true);
			if (_controller.InSight (_controller.player)) {
				logicFollowTimer = 0;
				agent.SetDestination (_controller.player.position);
			} else {
				logicFollowTimer += Time.deltaTime;
				if (logicFollowTimer >= logicFollowTime) {
					agent.SetDestination (lastKnownPos);
				} else {
					agent.SetDestination (_controller.player.position);
				}
			}
		} else {
			_controller.SetAnim ("run", false);
			agent.SetDestination (transform.position);
			attackTimer += Time.deltaTime;
			if (attackTimer >= currentAttackWait) {
				_controller.TransitionTo<AttackState> ();
			}
		}
	}

	public override void Exit (){
		_controller.SetAnim ("activeIdle", false);
	}

}

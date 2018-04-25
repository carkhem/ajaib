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
	[Header("Fighting")]
	public MinMaxFloat attackWait;
	private float currentAttackWait;
	private float timer = 0;

	private EnemyController _controller;


	public override void Initialize(Controller owner) {
		_controller = (EnemyController)owner;
		agent = transform.GetComponent<NavMeshAgent> ();
	}

	public override void Enter (){
		transform.GetComponent<NavMeshAgent> ().enabled = true;
		timer = 0;
		currentAttackWait = Random.Range (attackWait.Min, attackWait.Max);
		agent.speed = runSpeed;
	}

	public override void Update (){
		//Stannar för skarpt. Vill egentligen hitta en vector mellan tarnsform och player som är stopDistance ifrån playern.
		if (Vector3.Distance (transform.position, _controller.player.position) > stopDistance) {
			agent.SetDestination (_controller.player.position);
		} else {
			agent.SetDestination (transform.position);
			timer += Time.deltaTime;
			if (timer >= currentAttackWait) {
				_controller.TransitionTo<AttackState> ();
			}
		}
		transform.LookAt (new Vector3 (_controller.player.position.x, transform.position.y, _controller.player.position.z));
	}
}

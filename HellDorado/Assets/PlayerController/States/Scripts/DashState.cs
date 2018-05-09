using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/States/Dash")]
public class DashState : State {

	public float dashDuration = 1f;
	public float xDashSpeed = 2f;
	public float yDashSpeed = 1f;
	private float timer = 0;
	private float xDir;
	private float yDir;

	private PlayerController _controller;

	public override void Initialize(Controller owner) {
		_controller = (PlayerController)owner;
	}

	public override void Enter (){
		_controller.lArmAnim.SetTrigger ("dash");
		xDir = Input.GetAxisRaw ("Horizontal");
		yDir = Input.GetAxisRaw ("Vertical");
		timer = 0;
	}

	public override void Update (){
		timer += Time.deltaTime;
		if (timer < dashDuration) {
//			Debug.Log ("dash");
			_controller.GetComponent<CharacterController>().Move(xDashSpeed * transform.right * xDir);
			_controller.GetComponent<CharacterController>().Move(yDashSpeed * transform.forward * yDir);
		} else
			_controller.TransitionTo<GroundState> ();
	}

}

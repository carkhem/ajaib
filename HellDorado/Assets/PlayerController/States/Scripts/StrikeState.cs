using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/States/Strike")]
public class StrikeState : State {

	private PlayerController _controller;

	public override void Initialize(Controller owner) {
		_controller = (PlayerController)owner;
	}

	public override void Enter (){
		_controller.rArmAnim.SetTrigger ("swing");
	}

	public override void Update (){
		_controller.CheckDash ();

		if (_controller.rArmAnim.GetCurrentAnimatorStateInfo (0).IsName ("SwordSwing 1")){
			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				_controller.rArmAnim.SetTrigger ("swing");
			}
		} else if (_controller.rArmAnim.GetCurrentAnimatorStateInfo (0).IsName("Idle")) {
			_controller.TransitionTo<GroundState> ();
		}
	}
}

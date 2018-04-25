using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/States/Air")]
public class AirState : State {

	private PlayerController _controller;

	public override void Initialize(Controller owner) {
		_controller = (PlayerController) owner;
	}

	public override void Update() {
		Velocity += Vector3.down * _controller.Gravity * Time.deltaTime;
		_controller.GetComponent<CharacterController>().Move(Velocity * Time.deltaTime);

		if (transform.GetComponent<CharacterController>().isGrounded){
			_controller.TransitionTo<GroundState> ();
		}
	}
}

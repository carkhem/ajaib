using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/States/Ground")]
public class GroundState : State {


	[Header("Jumping")]
	public float jumpForce = 10f;

	private PlayerController _controller;

	public override void Initialize(Controller owner) {
		_controller = (PlayerController)owner;
    }


	public override void Update() {
		_controller.GetComponent<CharacterController>().Move(_controller.Input * _controller.MaxSpeed * Time.deltaTime);
		_controller.Velocity.x = transform.GetComponent<CharacterController> ().velocity.x;
		_controller.Velocity.z = transform.GetComponent<CharacterController> ().velocity.z;

		if (!transform.GetComponent<CharacterController> ().isGrounded) {
			_controller.TransitionTo<AirState> ();
		}
		UpdateJump ();
	}
		

	private void UpdateMovement() {
		
	}
		

	private void UpdateJump() {
		if (Input.GetButtonDown ("Jump")) {
			_controller.Velocity.y = jumpForce;
		}
	}
}
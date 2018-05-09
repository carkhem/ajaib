using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/States/Ground")]
public class GroundState : State {

	public float Acceleration = 100f;

	[Header("Jumping")]
	public MinMaxFloat JumpHeight;
	[HideInInspector] public MinMaxFloat JumpVelocity;
	private PlayerController _controller;

	public override void Initialize(Controller owner) {
		_controller = (PlayerController)owner;
    }

	public override void Enter (){

	}

	public override void Update() {
        UpdateMovement ();
		_controller.CheckDash ();
		UpdateJump ();
		_controller.UpdateCrouch ();
		if (Input.GetButtonDown("Fire1") && _controller.rArmAnim.gameObject.activeSelf) {
			_controller.TransitionTo<StrikeState> ();
		}
	}

	private void UpdateJump() {
		if (Input.GetButtonDown ("Jump")) {
            _controller.Velocity.y = 10f;
			_controller.GetComponent<PlayerSounds> ().PlayJumpSound ();
        }
	}

	private void UpdateMovement() {
		_controller.GetComponent<CharacterController>().Move(_controller.InputVector * _controller.movementSpeed * Time.deltaTime);
		_controller.Velocity.x = transform.GetComponent<CharacterController> ().velocity.x;
		_controller.Velocity.z = transform.GetComponent<CharacterController> ().velocity.z;

		if (!transform.GetComponent<CharacterController> ().isGrounded) {
			_controller.TransitionTo<AirState> ();
		}
	}
}
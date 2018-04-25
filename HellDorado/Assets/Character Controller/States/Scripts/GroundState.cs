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
		Debug.Log ("Grounded State");
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
			Debug.Log ("JUMP!");
			_controller.Velocity.y = 10f;
		}
	}
}
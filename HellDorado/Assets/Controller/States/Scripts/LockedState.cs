using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/States/Locked")]
public class LockedState : State {

	public float TimeToJumpApex = 0.5f;
	public float InitialJumpDistance = 0.15f;
	[HideInInspector] public MinMaxFloat JumpVelocity;

	public GameObject target;

	private PlayerController _controller;

	public override void Enter ()
	{
		Debug.Log ("Locked State");
	}

	public override void Initialize(Controller owner) {
		_controller = (PlayerController) owner;
	}

	public override void Update() {

		UpdateMovement ();
		UpdateJump ();
	}

	private void UpdateMovement() {

	}

	private void UpdateJump(){

		if (!Input.GetButtonDown ("Jump"))
			return;
		transform.position += Vector3.up * InitialJumpDistance;
		Velocity = new Vector3(Velocity.x, JumpVelocity.Max, Velocity.z);
		_controller.GetState<AirState>().CanCancelJump = true;
		_controller.TransitionTo<AirState>();
	}

	private void LeaveState() {
		if (!Input.GetButton ("Fire2"))
			return;
		_controller.TransitionTo<GroundState> ();
	}
}

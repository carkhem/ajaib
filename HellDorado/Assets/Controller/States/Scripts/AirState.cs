using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/States/Air")]
public class AirState : State {

	public float FastFallingModifier = 2f;
	[HideInInspector] public bool CanCancelJump;

	[Header("Movement")]
	public float Acceleration = 50f;
	public float Friction = 5f;

	private PlayerController _controller;

	public override void Initialize(Controller owner) {
		_controller = (PlayerController) owner;
	}

	public override void Update() {
		UpdateGravity();
		UpdateMovement ();
		UpdateCollision();
		transform.position += Velocity * Time.deltaTime;
	}

	private void UpdateGravity() {
		float multiplier = Velocity.y > 0.0f ? 1.0f : FastFallingModifier;
		Velocity += Vector3.down * _controller.Gravity * multiplier * Time.deltaTime;
	}

	private void UpdateCollision() {
		RaycastHit[] hits = _controller.DetectHits();
		foreach (RaycastHit hit in hits)
		{
			if (MathHelper.CheckAllowedSlope(_controller.SlopeTollerance, hit.normal))
				_controller.TransitionTo<GroundState>();
			if (MathHelper.GetWallAngleDelta(hit.normal) < _controller.MaxWallAngleDelta)
				_controller.TransitionTo<WallState>();
			Velocity += MathHelper.GetNormalForce(Velocity, hit.normal);
			_controller.SnapToHit(hit);
		}
	}

	private void CancelJump() {
		float minJumpVelocity = _controller.GetState<GroundState>().JumpVelocity.Min;
		if (Velocity.y < minJumpVelocity) CanCancelJump = false;
		if (!CanCancelJump || Input.GetButton("Jump")) return;
		CanCancelJump = false;
		_controller.Velocity.y = minJumpVelocity;
	}

	private void UpdateMovement() {
		Vector3 input = _controller.Input;
		if (input.magnitude > _controller.InputRequiredToMove)
			Accelerate(input);
		else
			Decelerate();
	}

	private void Accelerate(Vector3 input) {
		Vector3 delta = input * Acceleration * Time.deltaTime;
		float y = Velocity.y;
		_controller.Velocity.y = 0.0f;
		if ((Velocity + delta).magnitude < _controller.MaxSpeed || Vector3.Dot(Velocity.normalized, input.normalized) < 0.0f)
			Velocity += delta;
		else
			Velocity = input.normalized * _controller.MaxSpeed;
		_controller.Velocity.y = y;
	}

	private void Decelerate() {
		float y = Velocity.y;
		_controller.Velocity.y = 0.0f;
		Velocity -= Velocity * Mathf.Clamp01(Friction * Time.deltaTime);
		_controller.Velocity.y = y;
	}
}

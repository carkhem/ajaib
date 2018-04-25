using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/States/Wall")]
public class WallState : State
{
	public float SlideSpeed = 5f;
	public float WallCheckDistance = 0.15f;
	public Vector2 JumpSpeed;
	public float InitialWallJumpDistance = 0.1f;
	private Vector3 _wallNormal;

	private PlayerController _controller;


	public override void Initialize(Controller owner) {
		_controller = (PlayerController) owner;
	}

	public override void Update() {
		RaycastHit[] hits = _controller.DetectHits(true, _controller.Input * WallCheckDistance);
		if (hits.Length == 0) {
			_controller.TransitionTo<AirState>();
			return;
		}
		UpdateCollision(hits);
		UpdateMovement();
		UpdateNormalForce(hits.ToList());
		UpdateJump();
		transform.position += Velocity * Time.deltaTime;
	}


	private void UpdateJump() {
		if (!Input.GetButtonDown("Jump") || _wallNormal.magnitude < MathHelper.FloatEpsilon)
			return;
		Velocity = _wallNormal * JumpSpeed.x;
		_controller.Velocity.y = JumpSpeed.y;
		transform.position += _wallNormal * InitialWallJumpDistance;
		_controller.TransitionTo<AirState>();
	}

	private void UpdateCollision(RaycastHit[] hits) {
		int wallHits = 0;
		_wallNormal = Vector3.zero;
		foreach (RaycastHit hit in hits) {
			if (MathHelper.GetWallAngleDelta(hit.normal) > _controller.MaxWallAngleDelta)
				continue;
			wallHits++;
			_wallNormal += hit.normal;
		}
		if (wallHits == 0)
			_controller.TransitionTo<AirState>();
		else
			_wallNormal /= wallHits;
	}

	private void UpdateMovement() {
		if (_wallNormal.magnitude < MathHelper.FloatEpsilon) return;
		Quaternion rotation = Quaternion.FromToRotation(Vector3.up, _wallNormal);
		Vector3 vector = rotation * _wallNormal;
		Velocity = vector.normalized * SlideSpeed;
	}

	private void UpdateNormalForce(List<RaycastHit> hits) {
		RaycastHit[] groundCheck = _controller.DetectHits (true);
		hits.AddRange (groundCheck);
		foreach (RaycastHit hit in hits) {
			_controller.SnapToHit (hit);
			Velocity += MathHelper.GetNormalForce (Velocity, hit.normal);
			if (MathHelper.CheckAllowedSlope (_controller.SlopeTollerance, hit.normal))
				_controller.TransitionTo<GroundState> ();
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/States/Ground")]
public class GroundState : State {

	public float Acceleration = 100f;
	public float ExtraFriction = 30f;
	private float Friction{ get{ return Acceleration / _controller.MaxSpeed; } }
	public float StopSlidingLimit = 1.5f;

	[Header("Jumping")]
	public MinMaxFloat JumpHeight;
	public float TimeToJumpApex = 0.5f;
	public float InitialJumpDistance = 0.15f;
	[HideInInspector] public MinMaxFloat JumpVelocity;

	[Header("EnemyCast")]
	public float distance;
	private RaycastHit rayHit;

	private PlayerController _controller;
	private Vector3 _groundNormal;
	private Vector3 _velocityBeforeNormalForce;

	public override void Initialize(Controller owner) {
		_controller = (PlayerController)owner;
		_controller.Gravity = (2 * JumpHeight.Max) / Mathf.Pow(TimeToJumpApex, 2);
		JumpVelocity.Max = _controller.Gravity * TimeToJumpApex;
		JumpVelocity.Min = Mathf.Sqrt(2 * _controller.Gravity * JumpHeight.Min);
	}

	public override void Update() {

		UpdateLock();
		UpdateJump();
		UpdateGravity();
		RaycastHit[] hits = _controller.DetectHits(true);
		if (hits.Length == 0)
		{
			_controller.TransitionTo<AirState>();
			return;
		}
		UpdateCollisions(hits);
		UpdateMovement();
		hits = _controller.DetectHits(true);
		UpdateFriction();
		_velocityBeforeNormalForce = Velocity;
		UpdateNormalForce(hits);
		transform.position += (Velocity * Time.deltaTime);
	}

	private void UpdateGravity() {
		Velocity += Vector3.down * _controller.Gravity * Time.deltaTime;
	}

	private void UpdateCollisions(RaycastHit[] hits) {
		_groundNormal = Vector3.zero;
		int groundHits = 0;

		foreach (RaycastHit hit in hits) {
			if (!MathHelper.CheckAllowedSlope (_controller.SlopeTollerance, hit.normal))
				continue;
			_groundNormal += hit.normal;
			groundHits++;
		}

		if (groundHits == 0)
			_controller.TransitionTo<AirState> ();
		else
			_groundNormal /= groundHits;
	}

	private void UpdateNormalForce(RaycastHit[] hits) {
		foreach (RaycastHit hit in hits) {
			_controller.SnapToHit(hit);
			Velocity += MathHelper.GetNormalForce(Velocity, hit.normal);
		}
	}

	private Vector3 ForwardAlongGround {
		get
		{
			float y = Camera.main.transform.rotation.eulerAngles.y;
			return Quaternion.Euler(90f, y, 0.0f) * _groundNormal;
		}
	}

	private void UpdateMovement() {
		Vector3 input = _controller.Input;
		if (input.magnitude < _controller.InputRequiredToMove) return;

		Vector3 cameraForward = Camera.main.transform.forward;
		cameraForward.y = 0.0f;
		float angle = Vector3.SignedAngle(input, cameraForward, Vector3.up);
		Vector3 delta = Quaternion.AngleAxis(angle, -_groundNormal) * ForwardAlongGround * Acceleration * Time.deltaTime;
		Velocity += delta;
	}

	private void UpdateFriction()
	{
		float extraFriction = _controller.Input.magnitude < _controller.InputRequiredToMove ? ExtraFriction : 0.0f;
		float friction = Mathf.Clamp01((Friction + extraFriction) * Time.deltaTime);
		Velocity -= Velocity * friction;
	}

	private void StopSliding() {
		if (_velocityBeforeNormalForce.magnitude < StopSlidingLimit &&
			_controller.Input.magnitude < _controller.InputRequiredToMove)
			Velocity = Vector3.zero;
	}

	private void UpdateJump() {
		if(!Input.GetButtonDown("Jump")) return;
		transform.position += Vector3.up * InitialJumpDistance;
		Velocity = new Vector3(Velocity.x, JumpVelocity.Max, Velocity.z);
		_controller.GetState<AirState>().CanCancelJump = true;
		_controller.TransitionTo<AirState>();
		CameraShake.AddIntesity (5.0f);
	}

	private void UpdateLock(){

		Vector3 forward = Camera.main.transform.forward * distance;
		Debug.DrawRay(transform.position,forward,Color.green);

		if (!Input.GetButtonDown("Fire2"))
			return;
		if (Physics.Raycast (transform.position, (forward), out rayHit) && rayHit.collider.tag == "Enemy") {
			GameObject t = rayHit.collider.gameObject;
			Debug.Log ("Target = " + t);
			_controller.GetState<LockedState>().target = t;
			_controller.TransitionTo<LockedState> ();
		}
	}
}
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

	public override void Enter (){
//		Debug.Log ("Air State");
	}

	public override void Update() {
		Velocity += Vector3.down * _controller.Gravity * Time.deltaTime;
		_controller.GetComponent<CharacterController>().Move(Velocity * Time.deltaTime);

		if (transform.GetComponent<CharacterController>().isGrounded){
			_controller.TransitionTo<GroundState> ();
		}

//		UpdateRewind ();
	}

//	private void UpdateRewind(){
//		if (Input.GetKeyDown (KeyCode.Mouse0) && !_controller.GetComponent<AbilityManager>().isRewinding)
//			_controller.TransitionTo<RewindState> ();
//	}
}

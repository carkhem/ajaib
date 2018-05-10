using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/States/Air")]
public class AirState : State {

	private float MinVelocityY = -10f;
	public float FastFallingModifier = 2f;
	[HideInInspector] public bool CanCancelJump;

	[Header("Movement")]
	public float Acceleration = 50f;
	public float Friction = 5f;

	private PlayerController _controller;
	private PlayerSounds ps;

	public override void Initialize(Controller owner) {
		_controller = (PlayerController) owner;
	}

	public override void Enter (){
//		Debug.Log ("Air State");
		ps = _controller.GetComponent<PlayerSounds> ();
//		_controller.Velocity.y = 0;
	}

	public override void Update() {
		if (Velocity.y < MinVelocityY) {
//			_controller.Velocity.y = MinVelocityY;
			Velocity += Vector3.down * _controller.gravity * FastFallingModifier * Time.deltaTime;
		} else {
			Velocity += Vector3.down * _controller.gravity * Time.deltaTime;

		}

		_controller.GetComponent<CharacterController>().Move(Velocity * Time.deltaTime);
        if (Input.GetButtonUp("Crouch")){
            _controller.GetComponent<PlayerController>().StopCrouch();
        }

        if (transform.GetComponent<CharacterController>().isGrounded){
			ps.PlayLandingSound ();
			_controller.TransitionTo<GroundState> ();
		}
        //		UpdateRewind ();
    }
    

//	private void UpdateRewind(){
//		if (Input.GetKeyDown (KeyCode.Mouse0) && !_controller.GetComponent<AbilityManager>().isRewinding)
//			_controller.TransitionTo<RewindState> ();
//	}
}

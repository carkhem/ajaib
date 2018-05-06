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
//     	Debug.Log ("Ground State");
    }

	public override void Update() {
        UpdateMovement ();
		_controller.CheckDash ();
		UpdateJump ();
        CheckPlayerLife();
		_controller.UpdateCrouch ();
		if (Input.GetButtonDown("Fire1")) {
			_controller.TransitionTo<StrikeState> ();
		}
        _controller.sprintCheat();
	}

	private void UpdateJump() {
		if (Input.GetButtonDown ("Jump")) {
            _controller.Velocity.y = 10f;
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

    private void CheckPlayerLife()
    {
       if( _controller.GetComponent<PlayerStats>().health <= 10)
        {
            GameManager.instance.GameOver();
            _controller.TransitionTo<DeathState>();
        }
    }
		

}
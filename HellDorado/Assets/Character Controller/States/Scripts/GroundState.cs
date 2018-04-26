﻿using System.Collections;
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


	public override void Update() {
		_controller.GetComponent<CharacterController>().Move(_controller.Input * _controller.MaxSpeed * Time.deltaTime);
		_controller.Velocity.x = transform.GetComponent<CharacterController> ().velocity.x;
		_controller.Velocity.z = transform.GetComponent<CharacterController> ().velocity.z;

		if (!transform.GetComponent<CharacterController> ().isGrounded) {
			_controller.TransitionTo<AirState> ();
		}
		UpdateJump ();

		RewindObjectAbility ();
		UseForcePush ();
		UpdateRewind ();
	}
		

	private void UpdateMovement() {
		
	}

	private void UseForcePush(){
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, 50f,_controller.ObjectLayer) && hit.collider.gameObject.tag == "ForcePush") {
			if (Input.GetKeyDown (KeyCode.F)) 
				_controller.GetComponent<ForcePush> ().ForcePushObject (hit);
			
		}
	}

	private void UpdateRewind(){
		if (Input.GetKeyDown (KeyCode.Mouse0) && !_controller.GetComponent<AbilityManager> ().isRewinding) {
			_controller.TransitionTo<RewindState> ();
			Debug.Log ("rewind from Groundstate");
		}
	}


	private void RewindObjectAbility ()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

	
		if (Physics.Raycast (ray, out hit, 50f, _controller.ObjectLayer)) {
			if (Input.GetKeyDown (KeyCode.R)) {
				hit.collider.gameObject.GetComponent<RewindObject> ().StartRewind ();
			} else if (Input.GetKeyUp (KeyCode.R)) {
				hit.collider.gameObject.GetComponent<RewindObject> ().StopRewind ();
			} else {
				Debug.Log ("GÖR");
			}
		} else if (!Physics.Raycast (ray, out hit, 50f, _controller.ObjectLayer)) {
			Debug.Log ("INTE");
		}
	}



	private void UpdateJump() {
		if (Input.GetButtonDown ("Jump")) {
			Debug.Log ("JUMP!");
			_controller.Velocity.y = 10f;
		}
	}


}
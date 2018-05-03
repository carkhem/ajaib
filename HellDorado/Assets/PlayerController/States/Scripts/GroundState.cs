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
     	Debug.Log ("Ground State");

    }

	public override void Update() {
        UpdateMovement ();
		UpdateJump ();
//        CheckPlayerLife();
		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			Debug.Log ("Swing sword");
			_controller.righArm.SetTrigger ("swing");
		}
        //		RewindObjectAbility (); ------ Gör sånt här i AbilityManager
        //		UseForcePush ();
        //		UpdateRewind ();
      

    }

	private void UpdateJump() {
		if (Input.GetButtonDown ("Jump")) {
            _controller.Velocity.y = 10f;
           

        }
	}
		

	private void UpdateMovement() {
		_controller.GetComponent<CharacterController>().Move(_controller.InputVector * _controller.MaxSpeed * Time.deltaTime);
		_controller.Velocity.x = transform.GetComponent<CharacterController> ().velocity.x;
		_controller.Velocity.z = transform.GetComponent<CharacterController> ().velocity.z;

		if (!transform.GetComponent<CharacterController> ().isGrounded) {
			_controller.TransitionTo<AirState> ();
		}
	}

//    private void CheckPlayerLife()
//    {
//       if( _controller.GetComponent<PlayerStats>().health <= 10)
//        {
//            GameManager.instance.GameOver();
//            _controller.TransitionTo<DeathState>();
//        }
//    }

 
    //	private void UseForcePush(){
    //		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
    //		RaycastHit hit;
    //
    //		if (Physics.Raycast (ray, out hit, 50f,_controller.ObjectLayer) && (hit.collider.gameObject.tag == "ForcePush" || hit.collider.gameObject.tag == "Enemy")) {
    //			if (Input.GetKeyDown (KeyCode.F)) 
    //				_controller.GetComponent<ForcePush> ().ForcePushObject (hit);
    //			
    //		}
    //	}

    //	private void UpdateRewind(){
    //		if (Input.GetAxisRaw ("Fire2") != 0 && !_controller.GetComponent<AbilityManager> ().isRewinding) {
    //			Debug.Log ("rewind from Groundstate");
    //			_controller.TransitionTo<RewindState> ();
    //		}
    //	}


    //	private void RewindObjectAbility (){
    //
    //		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
    //		RaycastHit hit;
    //
    //		if (Physics.Raycast (ray, out hit, 50f,_controller.ObjectLayer) && (hit.collider.gameObject.tag == "ForcePush")){
    //			if (Input.GetKeyDown (KeyCode.R)) {
    //				hit.collider.gameObject.GetComponent<RewindObject> ().DeactivateObject ();
    //				hit.collider.gameObject.GetComponent<RewindObject> ().StartRewind ();
    //			} else if (Input.GetKeyUp (KeyCode.R)) {
    //				hit.collider.gameObject.GetComponent<RewindObject> ().StopRewind ();
    //			} else if(hit.collider.gameObject.GetComponent<RewindObject>().deactivateObject == true){
    //				if(hit.collider.gameObject.GetComponent<RewindObject>().clone != null)
    //					Destroy(hit.collider.gameObject.GetComponent<RewindObject>().clone.gameObject);
    //			}else {
    //				hit.collider.gameObject.GetComponent<RewindObject> ().ActivateShadowObject ();
    //			}
    //		}
    //	}

}
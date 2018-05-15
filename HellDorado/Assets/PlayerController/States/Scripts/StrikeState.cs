using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/States/Strike")]
public class StrikeState : State {

	private PlayerController _controller;
	private bool swing2;
	private bool hit1 = false;
	public float reach = 2f;

	public override void Initialize(Controller owner) {
		_controller = (PlayerController)owner;
	}

	public override void Enter (){
		swing2 = true;
		hit1 = false;
		_controller.rArmAnim.SetTrigger ("swing1");
		_controller.rArmAnim.SetBool ("swing2", false);
		if (!_controller.rArmAnim.GetCurrentAnimatorStateInfo (0).IsName ("SwordSwing 1")) {
			_controller.GetComponent<PlayerSounds> ().PlaySwordSwing ();
		}
	}

	public override void Update (){
		_controller.CheckDash ();
		_controller.UpdateCrouch ();

		if (_controller.rArmAnim.GetCurrentAnimatorStateInfo (0).IsName ("SwordSwing 1")){
			if (_controller.rArmAnim.GetCurrentAnimatorStateInfo (0).normalizedTime < 0.7f && !hit1) {
				RaycastHit hit;
				if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out hit, reach)) {
					if (hit.transform.CompareTag("Enemy")){
						Debug.Log ("HIT");
						hit1 = true;
					}
				}
				Debug.DrawRay (transform.position, transform.TransformDirection (Vector3.forward) * reach, Color.yellow);
			}
			if (Input.GetButtonDown("Fire1")) {
				_controller.rArmAnim.SetBool ("swing2", true);
				if (!_controller.rArmAnim.GetCurrentAnimatorStateInfo (0).IsName ("SwordSwing 2") && swing2) {
					_controller.GetComponent<PlayerSounds> ().PlaySwordSwing ();
					swing2 = false;
				}
			}

		} else if (_controller.rArmAnim.GetCurrentAnimatorStateInfo (0).IsName("SwordIdle")) {
			_controller.TransitionTo<GroundState> ();
		}
	}
}

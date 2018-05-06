using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePush : MonoBehaviour {


	private Rigidbody rb;
	private BoxCollider objectCollider;
	private PlayerController _controller;
	private float timer = 0.05f;
	private bool force = false;
	public float range = 2f;

	// Use this for initialization
	void Start () {
		_controller = GetComponent<PlayerController> ();
	}
		

	void FixedUpdate(){

		if (rb != null && rb.velocity.sqrMagnitude < 0.01f && force) {
			TimerRigidbodyConstraints ();
		}
			
	}
	
	public void ForcePushObject(){

		Ray ray = Camera.main.ScreenPointToRay(new Vector3 (Screen.width / 2, Screen.height / 2, 0));
		RaycastHit hit;

		if ((Physics.Raycast (ray, out hit, range))) {
			if (hit.collider.gameObject.tag == "Interactable") {
				if (hit.collider.GetComponent<Rigidbody> () != null) {
					GetComponent<AbilitySounds> ().PlayAbilitySound ("Push");
					rb = hit.collider.gameObject.GetComponent<Rigidbody> ();
					rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
					rb.isKinematic = false;
					force = true;
					if (rb.velocity.sqrMagnitude < 0.01f) {
						rb.AddForce (_controller.transform.forward * 500f);
					}
				}
			} else if (hit.transform.CompareTag ("Enemy")) {
				hit.transform.GetComponent<EnemyController> ().TransitionTo<StunnedState> ();
			}
		}
	}

	void ForcePushEnemy(){
		
	}

	void TimerRigidbodyConstraints(){
		timer -= Time.deltaTime;

		if (timer <= 0f) {
			rb.constraints = RigidbodyConstraints.None;
			timer = 0.05f;
			force = false;
		}
	}
}

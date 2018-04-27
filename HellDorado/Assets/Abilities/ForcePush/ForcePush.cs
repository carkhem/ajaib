using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePush : MonoBehaviour {


	private Rigidbody rb;
	private PlayerController _controller;

	// Use this for initialization
	void Start () {
		_controller = GetComponent<PlayerController> ();
	}
		
	
	public void ForcePushObject(int forcePushCost){

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		if ((Physics.Raycast (ray, out hit, 50f) && (hit.collider.gameObject.tag == "ForcePush"))) {
			if (Input.GetKeyDown (KeyCode.Mouse1)) {
				rb = hit.collider.gameObject.GetComponent<Rigidbody> ();
				rb.isKinematic = false;
				if (rb.velocity.sqrMagnitude < 0.01f) {
					rb.AddForce (_controller.transform.forward * 500f);
					GetComponent<PlayerStats> ().ChangeHealth (-forcePushCost);
				}
					
				
			}
		}
	}

	void ForcePushEnemy(){
		
	}
}

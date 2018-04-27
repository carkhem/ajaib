﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePush : MonoBehaviour {


	private Rigidbody rb;
	private BoxCollider objectCollider;
	private PlayerController _controller;
	private float timer = 0.05f;
	private bool force = false;

	// Use this for initialization
	void Start () {
		_controller = GetComponent<PlayerController> ();
	}
		

	void FixedUpdate(){

		if (rb != null && rb.velocity.sqrMagnitude < 0.01f && force) {
			TimerRigidbodyConstraints ();
		}
			
	}
	
	public void ForcePushObject(int forcePushCost){

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		if ((Physics.Raycast (ray, out hit, 50f) && (hit.collider.gameObject.tag == "ForcePush"))) {
			if (Input.GetKeyDown (KeyCode.Mouse1)) {
				rb = hit.collider.gameObject.GetComponent<Rigidbody> ();
				rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
				rb.isKinematic = false;
				force = true;
				if (rb.velocity.sqrMagnitude < 0.01f) {
					rb.AddForce (_controller.transform.forward * 500f);
					GetComponent<PlayerStats> ().ChangeHealth (-forcePushCost);
				}
					
				
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

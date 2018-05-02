using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTime : MonoBehaviour {

	private string[] tags = { "Rigidbody", "Gate" };

	//För Gate
	private Animator animator;
	private Gate gate;
	private float gateDelayTemp = 0f;

	//För objekt som rör sig med Rigidbody, ex lådor
	private Rigidbody rb;

	private Vector3 VelocityBeforeFreeze;

	private bool setVelocity;
	public static bool freezeTime = false;

	// Use this for initialization
	void Start () {


		rb = GetComponent<Rigidbody> ();
		

		animator = GetComponent<Animator> ();
			if (animator != null)
				gate = GetComponentInParent<Gate> ();
		
	}
	
	// Update is called once per frame
	void Update () {

		if (rb != null) {
			if (freezeTime) {
				if (!setVelocity) {
					VelocityBeforeFreeze = rb.velocity;
					setVelocity = true;
				}
	
				rb.isKinematic = true;
		
			} else {
				if (setVelocity) {
					rb.velocity = VelocityBeforeFreeze;
					setVelocity = false;
				}
		
				rb.isKinematic = false;
			}
		}

		if (animator != null) {
			if (freezeTime) {
				if (gateDelayTemp == 0) {
					gateDelayTemp = gate.delayTime;
				}
				animator.speed = 0.1f;
				gate.delayTime = 0f;
			} else {
				if (gateDelayTemp != 0f) {
					gate.delayTime = gateDelayTemp;
					gateDelayTemp = 0f;
				}
				animator.speed = 1f;
			}

		}


	}
}

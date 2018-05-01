using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTime : MonoBehaviour {

	Rigidbody rb;
	Vector3 VelocityBeforeFreeze;

	bool setVelocity;
	public static bool freezeTime = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();	
	}
	
	// Update is called once per frame
	void Update () {


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
}

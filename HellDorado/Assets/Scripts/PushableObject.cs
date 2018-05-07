using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableObject : MonoBehaviour {
	private Vector3 newPos;
	private float force;
	private float angle;
	public float overridePushForce;

	public void Push (float angle, float force){
		this.force = force;
		this.angle = angle;
		if (overridePushForce > 0) {
			force = overridePushForce;
		}
		GetComponent<Rigidbody>().AddForce (Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward * force);
	}
}

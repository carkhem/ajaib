using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableObject : MonoBehaviour {
	private Vector3 newPos;
	private float force;
	private float angle;
	public float overridePushForce;

	public void Push (float angle, float force){
		this.angle = Mathf.Round(angle / 90) * 90;
		print (angle);
		if (overridePushForce > 0) {
			this.force = overridePushForce;
		} else {
			this.force = force;
		}
		GetComponent<Rigidbody>().AddForce (Quaternion.AngleAxis(transform.eulerAngles.y + angle, Vector3.up) * Vector3.forward * force);
	}
}

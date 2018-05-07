using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ropes : MonoBehaviour {

	Bridge bridge;

	// Use this for initialization
	void Start () {
		bridge = GetComponentInParent<Bridge> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider coll){

		if (coll.gameObject.tag == "FireBall") {
			bridge.counter += 1;
			Debug.Log ("Burn rope");
			Destroy (this.gameObject);
		}
	}
}

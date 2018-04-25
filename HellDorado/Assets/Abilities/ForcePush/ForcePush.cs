using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePush : MonoBehaviour {

	private Rigidbody rb;
	private GameObject gameObject;
	private PlayerController _controller;

	// Use this for initialization
	void Start () {
		_controller = GetComponent<PlayerController> ();
	}
		
	
	public void ForcePushObject(RaycastHit hit){

		rb = hit.collider.gameObject.GetComponent<Rigidbody> ();
		gameObject = GetComponent<GameObject> ();
		rb.isKinematic = false;
		rb.AddForce (_controller.transform.forward * 500f);

		Debug.Log ("PUSH");

	}

	void ForcePushEnemy(){
		
	}
}

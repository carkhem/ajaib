﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempAddForceScript : MonoBehaviour {

	private Rigidbody rb;

	Vector3 force = new Vector3(0,500,500);
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();

		rb.AddForce (force);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

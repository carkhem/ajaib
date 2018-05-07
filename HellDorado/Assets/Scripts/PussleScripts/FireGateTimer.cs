﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGateTimer : MonoBehaviour {

	public GameObject[] torches;
	[HideInInspector]
	public bool bothTorchesLit;

	void Update(){
		OpenGate ();
	}

	public void OpenGate(){
		foreach (GameObject t in torches) {
			if (!t.GetComponent<GateTorchTimer> ().lit) {
				return;
			} else {
				bothTorchesLit = true;
			}
		}

		if (!bothTorchesLit) {
			GetComponent<Gate> ().CloseGate ();
		} else {
			GetComponent<Gate> ().OpenGate ();

			foreach (GameObject t in torches) {
				t.GetComponent<GateTorchTimer> ().timer = 1f;
				t.GetComponent<GateTorchTimer> ().lit = true;
				Debug.Log ("keepLit");
			}
		}
	}
}

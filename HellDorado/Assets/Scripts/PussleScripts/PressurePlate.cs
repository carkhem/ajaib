﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour {

	public string[] interactableTags = {"Player", "Object"};
	public UnityEvent OnPressureEnter;
	public UnityEvent OnPressureExit;
	private int objects = 0;

	void OnTriggerEnter(Collider col){
		foreach (string t in interactableTags) {
			if (col.transform.CompareTag (t)) {
				objects++;
				Debug.Log ("Entered pressure plate");
				OnPressureEnter.Invoke ();
				break;
			}
		}
	}

	void OnTriggerExit(Collider col){
		foreach (string t in interactableTags) {
			if (col.transform.CompareTag (t)) {
				objects--;
				Debug.Log ("Exit pressure plate");
				if (objects == 0) {
					OnPressureExit.Invoke ();
				}
				break;
			}
		}
	}
}
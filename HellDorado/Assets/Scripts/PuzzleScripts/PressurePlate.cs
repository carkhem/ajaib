using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour {

	public string[] interactableTags = {"Player", "Interactable"};
	public UnityEvent OnPressureEnter;
	public UnityEvent OnPressureExit;
	private int objects = 0;

	void OnTriggerEnter(Collider col){
		foreach (string t in interactableTags) {
			if (col.transform.CompareTag (t)) {
				objects++;
				//GetComponent<Animator> ().SetBool ("activate", true);
				OnPressureEnter.Invoke ();
				break;
			}
		}
	}

	void OnTriggerExit(Collider col){
		foreach (string t in interactableTags) {
			if (col.transform.CompareTag (t)) {
				objects--;
				//GetComponent<Animator> ().SetBool ("activate", false);
				if (objects == 0) {
					OnPressureExit.Invoke ();
				}
				break;
			}
		}
	}
}

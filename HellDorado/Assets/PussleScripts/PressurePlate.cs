using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour {

	public string[] interactableTags = {"Player"};
	public UnityEvent OnPressureEnter;
	public UnityEvent OnPressureExit;
	private int objects = 0;

	void OnCollisionEnter(Collision col){
		foreach (string t in interactableTags) {
			if (col.transform.CompareTag (t)) {
				objects++;
				Debug.Log ("Entered pressure plate");
				OnPressureEnter.Invoke ();
				break;
			}
		}
	}

	void OnCollisionExit(Collision col){
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

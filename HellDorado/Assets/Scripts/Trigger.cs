using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour {
	public string[] interactableTags = {"Player"};
	public UnityEvent TriggerEnter;
	public UnityEvent TriggerExit;

	void OnTriggerEnter(Collider col){
		foreach (string t in interactableTags) {
			if (col.transform.CompareTag (t)) {
				TriggerEnter.Invoke ();
				break;
			}
		}
	}

	void OnTriggerExit(Collider col){
		foreach (string t in interactableTags) {
			if (col.transform.CompareTag (t)) {
				TriggerExit.Invoke ();
				break;
			}
		}
	}
}

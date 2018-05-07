using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGate : MonoBehaviour {
	public GameObject[] torches;
	private bool open;

	void Update(){
		OpenGate ();
	}

	public void OpenGate(){
		foreach (GameObject t in torches) {
			if (!t.GetComponent<GateTorch> ().lit) {
				if (open) {
					GetComponent<Gate> ().CloseGate ();
				}
				return;
			}
		}
		if (!open) {
			GetComponent<Gate> ().OpenGate ();
		}
	}
}

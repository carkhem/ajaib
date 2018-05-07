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
					open = false;
					GetComponent<Gate> ().CloseGate ();
				}
				return;
			}
		}
		if (!open) {
			open = true;
			GetComponent<Gate> ().OpenGate ();
		}
	}
}

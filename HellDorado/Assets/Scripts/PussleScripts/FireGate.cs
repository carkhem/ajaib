using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGate : MonoBehaviour {
	public GameObject[] torches;

	void Update(){
		OpenGate ();
	}

	public void OpenGate(){
		foreach (GameObject t in torches) {
			if (!t.GetComponent<GateTorch> ().lit) {
				GetComponent<Gate> ().CloseGate ();
				return;
			}
		}
		GetComponent<Gate> ().OpenGate ();
	}
}

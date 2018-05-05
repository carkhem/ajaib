using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodGate : MonoBehaviour {

	public GameObject[] bloodPodiums;

	void Update () {
		OpenGate ();
	}

	public void OpenGate(){
		foreach (GameObject g in bloodPodiums) {
			if (!g.GetComponent<BloodPodium>().filled) {
				GetComponent<Gate> ().CloseGate ();
				return;
			}
		}
		GetComponent<Gate> ().OpenGate ();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateLever : MonoBehaviour {

	public GameObject gate;
	public GameObject reversedGate;
	public bool open;

	void Update(){
		LiveLever ();
	}

	public void PullLever(){
		open = !open;
		LiveLever();
	}

	private void LiveLever(){
		if (open) {
			gate.GetComponent<Gate> ().OpenGate ();
			if (reversedGate != null)
				reversedGate.GetComponent<Gate> ().CloseGate ();
		} else {
			gate.GetComponent<Gate> ().CloseGate ();
			if (reversedGate != null)
				reversedGate.GetComponent<Gate> ().OpenGate ();
		}
	}

}

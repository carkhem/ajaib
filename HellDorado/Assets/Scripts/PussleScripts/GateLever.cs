using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateLever : MonoBehaviour {

	public AudioSource source;
	public AudioClip leverSound;
	public GameObject gate;
	public GameObject reversedGate;
	public bool open;

	void Start() {
		source = GetComponent<AudioSource> ();
	}

	void Update(){
		LiveLever ();
	}

	public void PullLever(){
		open = !open;
		LiveLever();
		source.PlayOneShot (leverSound);
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

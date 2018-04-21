using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatePressurePlate : MonoBehaviour {
	public GameObject gate;
	public string[] interactableTags = {"Player"};
	private float localTimer = 0;
	public float timer = 0;
	public bool pressed = false;

	void Update(){
		if (pressed) {
			gate.GetComponent<Gate> ().OpenGate ();
			localTimer = 0;
		} else {
			if (localTimer > timer) {
				gate.GetComponent<Gate> ().CloseGate ();
			} else {
				localTimer += Time.deltaTime;
			}
		}
	}

	void OnCollisionEnter (Collision col){
		foreach (string t in interactableTags) {
			if (col.gameObject.CompareTag (t)) {
				pressed = true;
			}
		}
	}

	void OnCollisionExit (Collision col){
		foreach (string t in interactableTags) {
			if (col.gameObject.CompareTag (t)) {
				pressed = false;
			}
		}
	}
}

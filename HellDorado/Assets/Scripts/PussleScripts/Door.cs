using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	private Animator anim;
	public GameObject[] keys;

	void Awake(){
		anim = GetComponent<Animator> ();
	}

	public void OpenDoor(){
		for (int i = 0; i < keys.Length; i++) {
			if (keys [i] != null) {
				return;
			}
		}
		anim.SetBool ("open", true);
	}

}

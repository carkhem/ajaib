using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	private Animator anim;
	private bool open;
	public GameObject[] keys;

	void Awake(){
		anim = GetComponent<Animator> ();
	}

	public void ToggleDoor(){
		if (open) {
			CloseDoor ();
		} else {
			OpenDoor ();
		}
	}

	public void OpenDoor(){
		for (int i = 0; i < keys.Length; i++) {
			if (keys [i] != null) {
				return;
			}
		}
		anim.SetBool ("open", true);
		open = true;
	}

	public void CloseDoor(){
		anim.SetBool ("open", false);
		open = false;
	}

}

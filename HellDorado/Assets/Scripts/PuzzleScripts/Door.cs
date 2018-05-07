using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	public AudioClip doorSound;
	private AudioSource source;
	private Animator anim;
	private bool open;
	public GameObject[] keys;

	void Awake(){
		anim = GetComponent<Animator> ();
		source = GetComponent<AudioSource> ();
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
		PlayDoorSound ();
	}

	public void CloseDoor(){
		anim.SetBool ("open", false);
		open = false;
		PlayDoorSound ();
	}

	public void PlayDoorSound(){
		source.PlayOneShot (doorSound);
	}
}

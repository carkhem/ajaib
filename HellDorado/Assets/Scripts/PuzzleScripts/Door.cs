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

	void Update(){
		bool allKeys = true;
		foreach (GameObject key in keys) {
			if (key != null)
				allKeys = false;
		}

		if (allKeys) {
			GetComponent<InteractableObject> ().showText = true;
		} else {
			GetComponent<InteractableObject> ().showText = false;
		}
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

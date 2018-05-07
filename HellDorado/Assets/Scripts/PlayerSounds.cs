using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour {

	public AudioSource source;
	public AudioClip dashSound;
	public AudioClip swordSwing;
	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlaySwordSwing(){
//		if (source.isPlaying) {
//			StopPlayAudio ();
//		}
//
//		if (!source.isPlaying) {
			source.PlayOneShot (swordSwing);
//		}
	}

	public void PlayDashSound(){
		source.PlayOneShot (dashSound);
	}

	public void StopPlayAudio(){
		source.Stop ();

	}
}

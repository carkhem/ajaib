using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySounds : MonoBehaviour {

	public AudioSource source;

	[Header("Rewind")]
	public AudioClip rewindClip;

	[Header("Fireball")]
	public AudioClip fireClip;

	[Header("Push")]
	public AudioClip pushClip;

	[Header("RewindObject")]
	public AudioClip rewindObjectClip;

	void Start () {
		source = GetComponent<AudioSource> ();
	}


	public void PlayAbilitySound(string ability){
		switch (ability) {
		case "Rewind":
			PlayRewind ();
			break;
		case "RewindObject":
			PlayRewindObject ();
			break;
		case "Fireball":
			PlayFireball ();
			break;
		case "Push":
			PlayPush();
			break;
		default:
			break;
		}
	}

	private void PlayRewind(){
	
	}
		
	private void PlayRewindObject(){

	}

	private void PlayFireball(){
		if(!source.isPlaying)
			source.PlayOneShot (fireClip);
	}

	private void PlayPush(){
		if(!source.isPlaying)
			source.PlayOneShot (pushClip);
	}
}

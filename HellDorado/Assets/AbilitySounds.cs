using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySounds : MonoBehaviour {

	private AudioSource source;

	[Header("Rewind")]
	public AudioClip rewindClip;
	private bool rewinding;

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
		if(!source.isPlaying)
			source.PlayOneShot (rewindClip);
	}
		
	private void PlayRewindObject(){
			source.PlayOneShot (rewindClip);
	}

	private void PlayFireball(){
		StopPlayingAudio ();
		if(!source.isPlaying)
			source.PlayOneShot (fireClip);
	}

	private void PlayPush(){
		if(!source.isPlaying)
			source.PlayOneShot (pushClip);
	}

	public void StopPlayingAudio(){
		source.Stop ();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySounds : MonoBehaviour {

	private AudioSource abilitySource;
    private AudioSource[] sources;
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
        sources = GetComponents<AudioSource>();
        abilitySource = sources[0];
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
		if(!abilitySource.isPlaying)
			abilitySource.PlayOneShot (rewindClip);
	}
		
	private void PlayRewindObject(){
			abilitySource.PlayOneShot (rewindClip);
	}

	private void PlayFireball(){
		StopPlayingAudio ();
		if(!abilitySource.isPlaying)
			abilitySource.PlayOneShot (fireClip);
	}

	private void PlayPush(){
		if(!abilitySource.isPlaying)
			abilitySource.PlayOneShot (pushClip);
	}

	public void StopPlayingAudio(){
		abilitySource.Stop ();
	}
}

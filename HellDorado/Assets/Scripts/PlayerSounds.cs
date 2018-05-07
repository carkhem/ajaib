using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour {

	public AudioSource[] sources;

	public AudioClip swordSwing;
	public AudioClip deathSound;
	public AudioClip dashSound;
	public AudioClip[] takingDamageSounds;
	public AudioClip[] jumpSounds;
	public AudioClip[] walkingSounds;
	public AudioClip keySound;
	public AudioClip landingSound;

	private int clipIndex;

	void Start () {
		sources = GetComponents<AudioSource> ();
//		sources [0].clip = swordSwing;
//		sources [1].clip = dashSound;
//		sources [2].clip = jumpSounds;
//		sources [3].clip = takingDamageSounds;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlaySwordSwing(){
		sources[0].PlayOneShot (swordSwing);
	}

	public void PlayDashSound(){
		sources[1].PlayOneShot (dashSound);
	}

//	public void StopPlayAudio(){
//		source.Stop ();
//	}

	public void TakingDamgeSound(){
		if (!sources[3].isPlaying) 
			clipIndex = Random.Range (0, takingDamageSounds.Length);
			AudioClip clip = takingDamageSounds [clipIndex];
			sources[3].PlayOneShot (clip);
	}

	public void PlayDeathSound(){
		sources[0].PlayOneShot (deathSound);
	}

	public void PlayJumpSound (){
			clipIndex = Random.Range (1, jumpSounds.Length);
			AudioClip clip = jumpSounds [clipIndex];
			sources[2].PlayOneShot (clip);
			jumpSounds [clipIndex] = jumpSounds [0];
			jumpSounds [0] = clip;
	}

	public void PlayKeyPickUpSound(){
		sources [3].PlayOneShot (keySound);
	}

	public void PlayWalkSound(){
		clipIndex = Random.Range (1, walkingSounds.Length);
		AudioClip clip = walkingSounds [clipIndex];
		if (!sources [2].isPlaying) {
			sources [2].PlayOneShot (clip);
			walkingSounds [clipIndex] = walkingSounds [0];
			walkingSounds [0] = clip;
		}
	}

	public void PlayLandingSound() {

	}
}
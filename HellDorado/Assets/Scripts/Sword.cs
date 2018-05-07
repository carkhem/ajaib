using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

	public AudioSource source;
	public AudioClip[] enemyHitClips;
	//public AudioClip[] playerHitClips;
	public AudioClip playerHitClip;
	private int clipIndex;

	public Animator anim;
	public enum SwordTypes{
		Firendly, Hostile
	}
	public SwordTypes swordType = SwordTypes.Firendly;
	public float damage = 20;

	private void OnTriggerEnter (Collider col){
		if (col.CompareTag ("Enemy") && swordType == SwordTypes.Firendly) {
			if ((anim.GetCurrentAnimatorStateInfo (0).IsName ("SwordSwing 1") || anim.GetCurrentAnimatorStateInfo (0).IsName ("SwordSwing 2")) && (anim.GetCurrentAnimatorStateInfo (0).normalizedTime > 0 && anim.GetCurrentAnimatorStateInfo (0).normalizedTime < 0.7f)) {
				col.GetComponent<EnemyController> ().TakeDamage (PlayerStats.instance.meleeDamage);
				PlayEnemyHitSounds ();
			}
		}

		if (col.CompareTag ("Player") && swordType == SwordTypes.Hostile) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Attack") && (anim.GetCurrentAnimatorStateInfo (0).normalizedTime > 0.1f && anim.GetCurrentAnimatorStateInfo (0).normalizedTime < 0.6f)) {
				col.GetComponent<PlayerStats> ().ChangeHealth (-damage);
				PlayPlayerHitSounds ();
			}
		}
	}

	private void PlayEnemyHitSounds(){
		if (!source.isPlaying) {
			clipIndex = Random.Range (0, enemyHitClips.Length);
			AudioClip clip = enemyHitClips [clipIndex];
			source.PlayOneShot (clip);
		}
	}

	private void PlayPlayerHitSounds(){
		if (!source.isPlaying) {
			source.PlayOneShot (playerHitClip);
//			clipIndex = Random.Range (0, enemyHitClips.Length);
//			AudioClip clip = playerHitClips [clipIndex];
//			source.PlayOneShot (clip);

			//om vi lägger in fler ljud
		}
	}
}

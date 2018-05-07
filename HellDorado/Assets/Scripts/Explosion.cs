using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	public AudioClip[] explosionSounds;
	public AudioSource source;
	private int clipIndex;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
		clipIndex = Random.Range (0, explosionSounds.Length);
		AudioClip clip = explosionSounds [clipIndex];
		source.PlayOneShot(clip);
	}
	
	void Update () {
//		if (!GetComponent<ParticleSystem> ().IsAlive() && !GetComponent<AudioSource>().isPlaying) {
//			Destroy (gameObject);
//		}
		if(GetComponent<ParticleSystem>().isStopped && !GetComponent<AudioSource>().isPlaying){
			Destroy (gameObject);
		}
			
	}
}

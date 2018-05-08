using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour {

	public AudioClip swingSound;
	public AudioClip[] walkingSounds;
	private AudioSource source;
	private int clipIndex;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlaySwingSound(){
		if (!source.isPlaying) {
			source.PlayOneShot (swingSound);
		}
	}

	public void PlayWalkingSound() {
		if (!source.isPlaying) {
			clipIndex = Random.Range (1, walkingSounds.Length);
			AudioClip clip = walkingSounds [clipIndex];
			source.PlayOneShot (clip);
			walkingSounds [0] = clip;
		}
	}
}
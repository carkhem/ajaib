using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSound : MonoBehaviour {

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
	
	// Update is called once per frame
	void Update () {
		
	}
}

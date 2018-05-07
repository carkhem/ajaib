using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSound : MonoBehaviour {

	public AudioClip fireSound;
	private AudioSource source;


	void Start () {
		source = GetComponent<AudioSource> ();
		source.clip = fireSound;
		source.loop = true;
		source.time = Random.Range (0, fireSound.length);
		source.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

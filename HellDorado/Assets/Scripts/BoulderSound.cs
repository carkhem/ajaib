using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderSound : MonoBehaviour {

	public AudioClip boulderSound;
	private AudioSource source;

	void Start () {
		source = GetComponent<AudioSource> ();
		source.PlayOneShot (boulderSound);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

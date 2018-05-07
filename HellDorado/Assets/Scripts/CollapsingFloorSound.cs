using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsingFloorSound : MonoBehaviour {

	public AudioClip collapsingSound;
	private AudioSource source;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
	}
		
	private void OnTriggerEnter(Collider col) {
		if(col.gameObject.tag == "Player") {
			source.PlayOneShot(collapsingSound);

		}
	}
}

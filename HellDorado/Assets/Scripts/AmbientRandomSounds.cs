﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientRandomSounds : MonoBehaviour {

	public float minDelay, maxDelay;
	public AudioClip ambient1, ambient2, ambient3, ambient4, ambient5;
	private AudioSource[] sources;

	// Use this for initialization
	void Start () {
		sources = GetComponents<AudioSource> ();
		sources [0].clip = ambient1;
		sources [1].clip = ambient2;
		sources [2].clip = ambient3;
		sources [3].clip = ambient4;
		sources [5].clip = ambient5;
	}
	
	// Update is called once per frame
	void Update () {

		if (!sources [0].isPlaying) {
			float d = Random.Range (minDelay, maxDelay);
			sources [0].PlayDelayed (d);
		}

		if (!sources [1].isPlaying) {
			float d = Random.Range (minDelay, maxDelay);
			sources [1].PlayDelayed (d);
		}

		if (!sources [2].isPlaying) {
			float d = Random.Range (minDelay, maxDelay);
			sources [2].PlayDelayed (d);
		}
	}
}

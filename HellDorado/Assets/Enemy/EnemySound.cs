using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour {

	public AudioClip swingSound;
	public AudioClip[] walkingSounds;
	private AudioSource[] sources;
	private int clipIndex;

	// Use this for initialization
	void Start () {
		sources = GetComponents<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlaySwingSound(){
		if (!sources[0].isPlaying) {
			sources[0].PlayOneShot (swingSound);
		}
	}

	public void PlayWalkingSound() {
		foreach (AudioSource s in sources) {
			if (!s.isPlaying) {
				clipIndex = Random.Range (1, walkingSounds.Length);
				AudioClip clip = walkingSounds [clipIndex];
				s.PlayOneShot (clip);
				walkingSounds [0] = clip;
				break;
			}
		}
	}
}
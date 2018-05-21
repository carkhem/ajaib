using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkWhisper : MonoBehaviour {

    public AudioClip clip;
    private AudioSource source;
    

	void Start () {
        source = GetComponent<AudioSource>();
	}
	
    public void PlayWhisper() {
        source.PlayOneShot(clip);
    }
}

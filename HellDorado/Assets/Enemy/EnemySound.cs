using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour {

    // Finns kvar pga av ljud vid animeringen i början av level1. Bör ersättas eller göras om.
    // EnemySounds är den som sköter alla andra ljud för fiender. Ja, namnen är förvirrande lika.

    //public AudioClip swingSound;
	public AudioClip[] walkingSounds;
   //public AudioClip[] hitSounds;
    private AudioSource[] sources;
	private int clipIndex;

	// Use this for initialization
	void Start () {
		sources = GetComponents<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //public void PlaySwingSound()
    //{
    //    if (!sources[0].isPlaying)
    //    {
    //        sources[0].PlayOneShot(swingSound);
    //    }
    //}

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
    
    public void StopPlayAudio() {
        foreach (AudioSource s in sources)
            if (s.isPlaying)
            {
                s.Stop();
            }
    }
}
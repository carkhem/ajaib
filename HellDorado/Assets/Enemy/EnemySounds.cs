using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour {

    public AudioClip[] walkingSounds;
    public AudioClip[] hitSounds;
    public AudioClip swingSound;
    public AudioClip deathSound;
    public AudioClip castSound;
    private AudioSource[] sources;
    private AudioSource hit, swing, death, walk;
    private int clipIndex;
    
    // Use this for initialization
	void Start () {
        sources = GetComponents<AudioSource>();
        //sources[0] = hit;
        //sources[1] = walk;
        //sources[2] = swing;
    }
	
	void Update () {
		
	}

    public void PlayHitSound()
    {
        if (hitSounds.Length > 0)
        {
            if (!sources[0].isPlaying)
            {
                clipIndex = Random.Range(1, hitSounds.Length);
                AudioClip clip = hitSounds[clipIndex];
                sources[0].PlayOneShot(clip);
                //sources[1].PlayOneShot(clip);
                //sources[2].PlayOneShot(clip);
                hitSounds[clipIndex] = hitSounds[0];
                hitSounds[0] = clip;
            }
        }
    }

    public void PlaySwingSound() {
            sources[2].PlayOneShot(swingSound);
    }       

    public void StopSwingSound() {
        if (sources[2].isPlaying)
        {
            swing.Stop();
        }
    }

    public void PlayCastSound()
    {
        sources[2].PlayOneShot(castSound);
    }

    public void PlayDeathSound() {
        sources[3].PlayOneShot(deathSound);
    }

    public void PlayWalkingSound() { }

    public void StopAllSound() {
        foreach (AudioSource s in sources)
        {
            if (s.isPlaying)
            {
                s.Stop();
                break;
            }
        }
    }
}

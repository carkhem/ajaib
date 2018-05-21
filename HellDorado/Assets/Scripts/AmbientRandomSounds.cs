using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientRandomSounds : MonoBehaviour
{

    public float minDelay, maxDelay;
    public AudioClip[] instrumentrual;
    public AudioClip[] voices;
    private AudioSource[] sources;
    private int currentSource = 0;
    private int clipIndex;

  
    void Start() {
        sources = GetComponents<AudioSource>();
    }

    void Update() {
        PlayAmbientSound();
    }

    private void PlayAmbientSound() {

        foreach (AudioSource s in sources)
        {
            if (s.isPlaying)
            {
                break;
            }
        }

        switch (currentSource)
        {
            case 0:
                PlayInstruemntrualAmbient();
                break;
            case 1:
                PlayVoiceAmbient();
                break;
        }
    }

    private void PlayInstruemntrualAmbient()
    {
        if (!sources[0].isPlaying)
        {
            clipIndex = 1;
            AudioClip rndClip = instrumentrual[clipIndex];
            sources[0].clip = rndClip;
            float d = Random.Range(minDelay, maxDelay);
            sources[0].PlayDelayed(d);
            instrumentrual[clipIndex] = instrumentrual[0];
            instrumentrual[0] = rndClip;
            currentSource = 1;
        }
    }

    private void PlayVoiceAmbient()
    {
        if (!sources[1].isPlaying)
        {
            if (voices.Length == 0)
                return;
            clipIndex = Random.Range(1, voices.Length);
            AudioClip rndClip = voices[clipIndex];
            sources[1].clip = rndClip;
            float d = Random.Range(minDelay, maxDelay);
            sources[1].PlayDelayed(d);
            voices[clipIndex] = voices[0];
            voices[0] = rndClip;
            currentSource = 0;
        }
    }
}
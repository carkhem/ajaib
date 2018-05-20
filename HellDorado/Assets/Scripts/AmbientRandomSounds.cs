using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientRandomSounds : MonoBehaviour
{

    public float minDelay, maxDelay;
    //	public AudioClip ambient1, ambient2, ambient3, ambient4, ambient5;
    public AudioClip[] instrumentrual;
    public AudioClip[] voices;
    private AudioSource[] sources;
    private int currentSource = 0;
    private int clipIndex;

    // Use this for initialization
    void Start()
    {
        sources = GetComponents<AudioSource>();
        //sources [0].clip = ambient1;
        //sources [1].clip = ambient2;
        //sources [2].clip = ambient3;
        //sources [3].clip = ambient4;
        //sources [4].clip = ambient5;
    }


    void Update()
    {
      //PlayAmbientSoundOld();
        PlayAmbientSound();
    }
    private void PlayAmbientSound()
    {

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
            clipIndex = Random.Range(1, instrumentrual.Length);
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

    public void PlayAmbientSoundOld()
    {
        if (!sources[0].isPlaying)
        {
            float d = Random.Range(minDelay, maxDelay);
            sources[0].PlayDelayed(d);
            //			Debug.Log ("s0 = " + d);
        }

        if (!sources[1].isPlaying)
        {
            float d = Random.Range(minDelay, maxDelay);
            sources[1].PlayDelayed(d);
            //			Debug.Log ("s1 = " + d);
        }

        if (!sources[2].isPlaying)
        {
            float d = Random.Range(minDelay, maxDelay);
            sources[2].PlayDelayed(d);
            //			Debug.Log ("s2 = " + d);
        }

        if (!sources[3].isPlaying)
        {
            float d = Random.Range(minDelay, maxDelay);
            sources[3].PlayDelayed(d);
            //			Debug.Log ("s3 = " + d);
        }

        if (!sources[4].isPlaying)
        {
            float d = Random.Range(minDelay, maxDelay);
            sources[4].PlayDelayed(d);
            //			Debug.Log ("s4 = " + d);
        }
    }
}
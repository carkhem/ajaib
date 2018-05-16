using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour {

    public AudioClip swordSwing;
    public AudioClip deathSound;
    public AudioClip dashSound;
    public AudioClip[] takingDamageSounds;
    public AudioClip[] jumpSounds;
    public AudioClip[] defaultWalkingSounds;
    public AudioClip[] bloodWalkingSounds;
    public AudioClip[] woodWalkingSounds;

    public AudioClip keySound;
    public AudioClip landingSound;

    private AudioSource[] sources; // 0 = death & swordSwing | 1 = dash | 2 = walking | 3 = takingDamage | 4 = jump
    private string surface;
    private int clipIndex;

    void Start()
    {
        sources = GetComponents<AudioSource>();
    }

    void Update()
    {
        surface = GetComponent<SurfaceIdentifier>().GetSurfaceType();
    }

    public void PlayDeathSound()
    {
        sources[0].PlayOneShot(deathSound);
    }

    public void PlaySwordSwing()
    {
        sources[2].Stop();
        sources[0].PlayOneShot(swordSwing);
    }

    public void PlayDashSound()
    {
        sources[1].PlayOneShot(dashSound);
    }

    public void TakingDamageSound()
    {
        if (!sources[3].isPlaying)
            clipIndex = Random.Range(0, takingDamageSounds.Length);
        AudioClip clip = takingDamageSounds[clipIndex];
        sources[3].PlayOneShot(clip);
    }

    public void PlayJumpSound()
    {
        clipIndex = Random.Range(1, jumpSounds.Length);
        AudioClip clip = jumpSounds[clipIndex];
        sources[4].PlayOneShot(clip);
        jumpSounds[clipIndex] = jumpSounds[0];
        jumpSounds[0] = clip;
    }
    public void PlayKeyPickUpSound()
    {
        sources[3].PlayOneShot(keySound);
    }

    public void PlayWalkSound() {
        //Kallas i Headbobber
        if (transform.GetComponent<CharacterController>().isGrounded)
        {

            if (gameObject.GetComponent<PlayerStats>().sneaking || sources[0].isPlaying)
            {
                return;
            }
            switch (surface)
            {
                case "Wood":
                    PlayWoodWalkSound();
                    break;
                case "Blood":
                    PlayBloodWalkSound();
                    break;
                case "Default":
                    PlayDefaultWalkSound();
                    break;
            }
        }
    }
    
        public void StopPlayWalkingSound()
    {
        sources[2].Stop();
    }

    public void PlayLandingSound()
    {
        if(!gameObject.GetComponent<PlayerStats>().sneaking) { 
            sources[1].PlayOneShot(landingSound);
        }
    }

    public void PlayDefaultWalkSound()
    {
        clipIndex = Random.Range(1, defaultWalkingSounds.Length);
        AudioClip clip = defaultWalkingSounds[clipIndex];
        if (!sources[2].isPlaying)
        {
            sources[2].PlayOneShot(clip);
            defaultWalkingSounds[clipIndex] = defaultWalkingSounds[0];
            defaultWalkingSounds[0] = clip;
        }
    }

    public void PlayWoodWalkSound()
    {
        clipIndex = Random.Range(1, woodWalkingSounds.Length);
        AudioClip clip = woodWalkingSounds[clipIndex];
        if (!sources[2].isPlaying)
        {
            sources[2].PlayOneShot(clip);
            woodWalkingSounds[clipIndex] = woodWalkingSounds[0];
            woodWalkingSounds[0] = clip;
        }
    }

    public void PlayBloodWalkSound()
    {
        clipIndex = Random.Range(1, bloodWalkingSounds.Length);
        AudioClip clip = bloodWalkingSounds[clipIndex];
        if (!sources[2].isPlaying)
        {
            sources[2].PlayOneShot(clip);
            bloodWalkingSounds[clipIndex] = bloodWalkingSounds[0];
            bloodWalkingSounds[0] = clip;
        }
    }
}
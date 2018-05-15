using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{

    public AudioSource[] sources;

    public AudioClip swordSwing;
    public AudioClip deathSound;
    public AudioClip dashSound;
    public AudioClip[] takingDamageSounds;
    public AudioClip[] jumpSounds;
    public AudioClip[] walkingSounds;
    public AudioClip keySound;
    public AudioClip landingSound;

    private int clipIndex;
    private Transform player;

    void Start()
    {
        sources = GetComponents<AudioSource>();
    }

    public void PlayDeathSound()
    {
        sources[0].PlayOneShot(deathSound);
    }

    public void PlaySwordSwing()
    {
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

    public void PlayWalkSound()
    {
        //Kallas i Headbobber
        if (transform.GetComponent<CharacterController>().isGrounded)
        {
            clipIndex = Random.Range(1, walkingSounds.Length);
            AudioClip clip = walkingSounds[clipIndex];
            if (!sources[2].isPlaying)
            {
                if (gameObject.GetComponent<PlayerStats>().sneaking)
                    return;
                sources[2].PlayOneShot(clip);
                walkingSounds[clipIndex] = walkingSounds[0];
                walkingSounds[0] = clip;
            }
        }
    }
    public void StopPlayWalkingSound()
    {
        sources[2].Stop();
    }

    public void PlayLandingSound()
    {
        sources[1].PlayOneShot(landingSound);
    }
}
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MushroomSounds : MonoBehaviour
{
    public AudioClip[] footStepClips;
    public AudioClip deathSound, hitSound, attackSound;

    private AudioSource aS;

    private void Start()
    {
        aS = GetComponent<AudioSource>();
    }

    public void AttackSound()
    {
        aS.PlayOneShot(attackSound);
    }

    public void FootstepSound()
    {
        aS.PlayOneShot(footStepClips[Random.Range(0, footStepClips.Length)]);
    }

    public void HitSound()
    {
        aS.PlayOneShot(hitSound);
    }

    public void DeathSound()
    {
        aS.PlayOneShot(deathSound);
    }
    
}

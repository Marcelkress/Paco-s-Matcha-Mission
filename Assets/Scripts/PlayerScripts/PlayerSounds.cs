using System;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayerSounds : MonoBehaviour
{
    [Title("FootSteps")]
    public AudioClip[] footStepSounds;

    [Title("Land sound")] public AudioClip landSound;

    private AudioSource audioSource;
    private CatInput playerInput;
    private bool wasInAir = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerInput = GetComponent<CatInput>();
    }

    public void PlayFootstepSound()
    {
        audioSource.PlayOneShot(footStepSounds[Random.Range(0, footStepSounds.Length)]);
    }

    private void LateUpdate()
    {
        bool isInAir = !playerInput.movement.collisions.below && !playerInput.wallSliding;
        
        // Detect landing
        if (wasInAir && !isInAir)
        {
            //CameraShake.instance.Shake(shakeIntensity, shakeTime);
            
            RaycastHit2D hit = Physics2D.Raycast( new(transform.position.x,transform.position.y - 0.5f), Vector2.down, 0.5f);
            if (hit)
            {
                //if (hit.transform.CompareTag("Dirt"))
                //{
                audioSource.PlayOneShot(landSound);
                //}
            }
        }
        wasInAir = isInAir;
    }
}

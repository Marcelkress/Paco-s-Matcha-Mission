using UnityEngine;
using UnityEngine.InputSystem;

public class Miaw : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] miawSounds;
    
    public void OnMiaw(InputValue value)
    {
        if (value.isPressed)
        {
            int randVal = Random.Range(0, miawSounds.Length);
            
            audioSource.PlayOneShot(miawSounds[randVal]);
        }
    }
}

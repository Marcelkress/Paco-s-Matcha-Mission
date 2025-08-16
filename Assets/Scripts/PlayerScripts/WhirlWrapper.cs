using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WhirlWrapper : MonoBehaviour
{
    public SpriteRenderer sprite;
    public AudioClip blueWhirl, redWhirl;
    public void SetSpriteActive()
    {
        sprite.enabled = true;
    }

    public void RedWhirlSound()
    {
        GetComponent<AudioSource>().PlayOneShot(redWhirl);
    }
    public void BlueWhirlSound()
    {
        GetComponent<AudioSource>().PlayOneShot(blueWhirl);
    }
}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

public class FadeLayer : MonoBehaviour
{
    public float fadeTime;
    public Tilemap tilemap;
    public AudioClip enterSound, exitSound;
    private AudioSource source;

    public Light2D[] caveLights;

    private float orgIntensity = 1f;
    private float t = 0;
    private Color tileColor;

    private void Start()
    {
        tileColor = tilemap.color;
        source = GetComponent<AudioSource>();
        foreach (Light2D light in caveLights)
        {
            light.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Fade(true));
            source.PlayOneShot(enterSound);
            MusicManager.instance.GetComponent<AudioLowPassFilter>().enabled = true;
            
            foreach (Light2D light in caveLights)
            {
                light.enabled = true;
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Fade(false));
            source.PlayOneShot(exitSound);
            MusicManager.instance.GetComponent<AudioLowPassFilter>().enabled = false;
            
            foreach (Light2D light in caveLights)
            {
                light.enabled = false;
            }
        }
    }

    private IEnumerator Fade(bool fadeAway)
    {
        float targetIntensity;
        float startIntensity = tilemap.color.a;
        t = 0f;
        
        if (fadeAway)
        {
            targetIntensity = 0;
        }
        else
        {
            targetIntensity = orgIntensity;
        }

        while (t < 1f)
        {
            float intensity = Mathf.Lerp(startIntensity, targetIntensity, t);
            tileColor.a = intensity;
            t += fadeTime * Time.deltaTime;

            tilemap.color = tileColor;
            
            yield return null;
        }

        tileColor.a = targetIntensity;
        tilemap.color = tileColor;
    }
}

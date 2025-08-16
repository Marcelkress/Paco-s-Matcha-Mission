using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TurnOffGlobalLight : MonoBehaviour
{
    public float fadeTime;
    public Light2D globalLight;

    private float orgIntensity;
    private float t = 0;
    
    private void Start()
    {
        orgIntensity = globalLight.intensity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FadeLight(true));
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FadeLight(false));
        }
    }

    private IEnumerator FadeLight(bool fadeAway)
    {
        float targetIntensity;
        float startIntensity = globalLight.intensity;
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
            globalLight.intensity = Mathf.Lerp(startIntensity, targetIntensity, t);
            t += fadeTime * Time.deltaTime;
            yield return null;
        }

        globalLight.intensity = targetIntensity;
    }
}

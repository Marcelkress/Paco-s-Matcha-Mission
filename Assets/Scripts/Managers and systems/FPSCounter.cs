using System;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private float updateInterval = 1.0f;
    private float timeSinceLastUpdate = 0f;
    private int frameCount = 0;
    private TMP_Text text;

    private bool on;
    
    private void Start()
    {
        text = GetComponent<TMP_Text>();
        on = true;
        Toggle();
    }

    public void Toggle()
    {
        if (on)
        {
            on = false;
            text.enabled = false;
        }
        else
        {
            on = true;
            text.enabled = true;
        }
    }
    
    private void Update()
    {
        if (!on)
        {
            return;
        }
        
        frameCount++;
        timeSinceLastUpdate += Time.unscaledDeltaTime;
        
        if (timeSinceLastUpdate > updateInterval)
        {
            float fps = frameCount / timeSinceLastUpdate;

            int fpsInt = Mathf.RoundToInt(fps);
            
            text.text = "FPS:" + fpsInt.ToString();
            
            frameCount = 0;
            timeSinceLastUpdate = 0;
        }
    }
}

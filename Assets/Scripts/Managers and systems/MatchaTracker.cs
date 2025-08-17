using System;
using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class MatchaTracker : MonoBehaviour
{
    [Title("Settings")]
    public int maxMatcha;
    public int currentHeldMatcha;
    
    [Title("UI References")]
    public TMP_Text UIText;
    public Image UIImage;
    public float fadeDuration, showDuration;
    public TMP_Text UICounterText;
    
    public static MatchaTracker instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHeldMatcha = 0;
    }
    
    public void CollectMatchaPiece()
    {
        StartCoroutine(ShowUnlockText());
        
        currentHeldMatcha++;

        UICounterText.text = "Matcha: ";

        UICounterText.text += currentHeldMatcha.ToString();

        UICounterText.text += "/10";
        
        if (currentHeldMatcha >= maxMatcha)
        {
            Debug.Log("Max Held");            
        }            
    }

    public void UseMatcha()
    {
        currentHeldMatcha--;

        UICounterText.text = "Matcha: ";

        UICounterText.text += currentHeldMatcha.ToString();

        UICounterText.text += "/10";
        
        if (currentHeldMatcha <= 0)
        {
            currentHeldMatcha = 0;
        } 
    }
    
    private IEnumerator ShowUnlockText()
    {
        UIText.DOFade(1, fadeDuration);
        UIImage.DOFade(1, fadeDuration);

        yield return new WaitForSeconds(showDuration);

        UIText.DOFade(0, fadeDuration);
        UIImage.DOFade(0, fadeDuration);
    }
    
}

using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class ShowIntroText : MonoBehaviour
{
    [Title("UI")]
    public TMP_Text text;
    public SpriteRenderer sprite1, sprite2;
    public float fadeDuration, showDuration;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ShowPrompt());
    }

    private IEnumerator ShowPrompt()
    {
        sprite1.DOFade(1, fadeDuration);
        sprite2.DOFade(1, fadeDuration);
        text.DOFade(1, fadeDuration);

        yield return new WaitForSeconds(showDuration);
        
        sprite1.DOFade(0, fadeDuration);
        sprite2.DOFade(0, fadeDuration);
        text.DOFade(0, fadeDuration);

    }
}

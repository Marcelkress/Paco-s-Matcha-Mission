using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class InteractPrompt : MonoBehaviour
{
    [Title("Prompt UI")]
    public TMP_Text promptText;
    public SpriteRenderer promptSprite, buttonSprite;
    public float fadeDuration;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            promptSprite.DOFade(1, fadeDuration);
            buttonSprite.DOFade(1, fadeDuration);
            promptText.DOFade(1, fadeDuration);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            promptSprite.DOFade(0, fadeDuration);
            buttonSprite.DOFade(0, fadeDuration);
            promptText.DOFade(0, fadeDuration);
        }
    }

}

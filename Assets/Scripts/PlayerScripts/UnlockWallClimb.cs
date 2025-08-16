using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class UnlockAbility : MonoBehaviour, IInteractable
{
    [Title("Unlocked UI")] public TMP_Text UIText;
    public TMP_Text smallerText;
    public Image UIImage;
    public float fadeDuration, showDuration;
    public AudioClip unlockClip;
    [ReadOnly]public bool opened = false;

    public GameObject magicSlider;

    public bool wallClimb, matchaMagic;
    
    public void Interact(Transform player)
    {
        if (opened)
            return;
        
        opened = true;
        GetComponent<Animator>().SetBool("IsOpened", opened);
        StartCoroutine(ShowUnlockText());
        
        if(wallClimb)
            player.GetComponent<CatInput>().canWallJump = true;

        if (matchaMagic)
        {
            player.GetComponent<MatchaMagic>().unlocked = true;
            magicSlider.SetActive(true);
        }

        TriggerSound sound = GetComponent<TriggerSound>();
        
        if (sound != null)
        {
            sound.PlayClip(unlockClip);
        }
        else
        {
            Debug.LogError("No TriggerSound script");
        }
    }

    private IEnumerator ShowUnlockText()
    {
        UIText.DOFade(1, fadeDuration);
        smallerText.DOFade(1, fadeDuration);
        UIImage.DOFade(1, fadeDuration);   
       
        yield return new WaitForSeconds(showDuration);

        UIText.DOFade(0, fadeDuration);
        smallerText.DOFade(0, fadeDuration);
        UIImage.DOFade(0, fadeDuration);
    }
}

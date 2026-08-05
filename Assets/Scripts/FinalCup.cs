using System;
using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class FinalCup : MonoBehaviour, IInteractable
{
    public GameObject matchaLeaf;
    public int maxLeafAmount = 10;
    public float timeBetweenLeafs = 0.5f;
    public string leafTag;
    public Animator anim;
    public Transform first, second;
    public float changeSceneDelay = 1.5f;

    [Title("UI")] public Image notEnoughUIBox;
    public TMP_Text notEnoughText;
    public float fadeDuration = 0.2f, showUITime = 3;
    public SpriteRenderer girlTextBox;
    public TMP_Text girlText;

    [Title("Sound")] public AudioClip soundEffectSingle;
    public AudioClip soundEffectAll;

    private AudioSource source;
    private int counter;
    private bool started;
    
    private void Start()
    {
        source = GetComponent<AudioSource>();
        counter = 0;
        started = false;
    }

    public void Interact(Transform cat)
    {
        if (MatchaTracker.instance.currentHeldMatcha < 10 && started == false)
        {
            started = true;
            notEnoughUIBox.DOFade(1, fadeDuration);
            notEnoughText.DOFade(1, fadeDuration);
            StartCoroutine(FadeAwayUI());
            return;
        }
        
        StartCoroutine(BeginAnimation(cat));
        gameObject.GetComponentInChildren<InteractPrompt>().gameObject.SetActive(false);
    }

    private IEnumerator FadeAwayUI()
    {
        yield return new WaitForSeconds(showUITime);
        
        notEnoughUIBox.DOFade(0, fadeDuration);
        notEnoughText.DOFade(0, fadeDuration);
    }

    private IEnumerator BeginAnimation(Transform parent)
    {
        for (int i = 0; i < maxLeafAmount; i++)
        {
            GameObject leaf = Instantiate(matchaLeaf, parent.position, quaternion.identity);
            JumpInCup leafScript = leaf.GetComponent<JumpInCup>();
            
            leafScript.targets[0] = first;
            leafScript.targets[1] = second;
            
            MatchaTracker.instance.UseMatcha();
            
            yield return new WaitForSeconds(timeBetweenLeafs);
        }

        if (MatchaTracker.instance.currentHeldMatcha == 0)
        {
            StartCoroutine(AllMatchaUsed());
        }
    }

    private IEnumerator AllMatchaUsed()
    {
        girlText.DOFade(1, fadeDuration);
        girlTextBox.DOFade(1, fadeDuration);

        yield return new WaitForSeconds(changeSceneDelay);
        
        SceneManager.instance.ChangeScene();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(leafTag))
        {
            anim.SetTrigger("Burst");
            counter++;

            if (counter == 10)
            {
                source.PlayOneShot(soundEffectAll);
            }
            else
            {
                source.PlayOneShot(soundEffectSingle);
            }
            
            Destroy(other.gameObject);
        }
    }
}

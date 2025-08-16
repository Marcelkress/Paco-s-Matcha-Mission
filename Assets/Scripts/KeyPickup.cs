using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class KeyPickup : MonoBehaviour, IInteractable
{
    private Rigidbody2D rb;
    private bool pickedUp;

    [Title("UI refs")] public SpriteRenderer box;
    public SpriteRenderer buttonIcon;
    public TMP_Text text;
    public float fadeDuration = 0.2f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pickedUp = false;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            box.DOFade(1, fadeDuration);
            buttonIcon.DOFade(1, fadeDuration);
            text.DOFade(1, fadeDuration); 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            box.DOFade(0, fadeDuration);
            buttonIcon.DOFade(0, fadeDuration);
            text.DOFade(0, fadeDuration);
        }
    }

    public void Interact(Transform parent)
    {
        // Pickup key
        if (!pickedUp)
        {
            pickedUp = true;
            rb.simulated = false;
            transform.SetParent(parent);
            transform.localPosition = Vector3.zero;
            
            box.DOFade(0, fadeDuration);
            buttonIcon.DOFade(0, fadeDuration);
            text.DOFade(0, fadeDuration);
        }
        else
        {
            Debug.Log("Drop object");
            pickedUp = false;
            rb.simulated = true;
            gameObject.transform.parent = null;
        }
    }
}

public interface IInteractable
{
    public void Interact(Transform parent);
}

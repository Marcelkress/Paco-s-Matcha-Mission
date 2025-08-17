using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class KeyPickup : MonoBehaviour, IInteractable
{
    private Rigidbody2D rb;
    private bool pickedUp;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pickedUp = false;
        rb = GetComponent<Rigidbody2D>();
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
            
            gameObject.GetComponentInChildren<InteractPrompt>().HidePrompt();
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

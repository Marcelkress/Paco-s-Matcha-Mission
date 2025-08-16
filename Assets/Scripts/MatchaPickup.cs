using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MatchaPickup : MonoBehaviour, IInteractable
{
    public AudioClip pickupClip;
    public int healAmount = 1;

    public void Interact(Transform trans)
    {
        MatchaTracker.instance.CollectMatchaPiece();
        trans.GetComponent<PlayerHealth>().Heal(healAmount);
        
        TriggerSound sound = GetComponent<TriggerSound>();
        if (sound != null)
        {
            sound.PlayClip(pickupClip);
        }
        else
        {
            Debug.LogError("No TriggerSound script");
        }
        
        Destroy(gameObject, 0.8f);
    }
}

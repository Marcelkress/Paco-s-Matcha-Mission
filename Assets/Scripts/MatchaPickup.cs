using UnityEngine;

public class MatchaPickup : MonoBehaviour, IInteractable
{
    public AudioClip pickupClip;
    public int healAmount = 1;
    private bool used = false;
    
    public void Interact(Transform trans)
    {
        if (used)
            return;

        used = true;
        
        MatchaTracker.instance.CollectMatchaPiece();
        trans.GetComponent<PlayerHealth>().Heal(healAmount);
        TriggerSound sound = GetComponent<TriggerSound>();
        gameObject.GetComponentInChildren<InteractPrompt>().HidePrompt();
        
        if (sound != null)
        {
            sound.PlayClip(pickupClip);
        }
        else
        {
            Debug.LogError("No TriggerSound script");
        }
        
        Destroy(gameObject, 0.5f);
    }
}

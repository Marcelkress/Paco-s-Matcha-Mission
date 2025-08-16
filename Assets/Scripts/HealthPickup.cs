using UnityEngine;

[RequireComponent (typeof(Collider2D))]
public class HealthPickup : MonoBehaviour
{
    public int healAmount;
    private bool used = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        IHealth health = other.transform.GetComponent<IHealth>();

        if (health != null && !used)
        {
            used = true;
            
            health.Heal(healAmount);
            
            GetComponent<Animator>().SetTrigger("Disappear");
        }
    }
}

using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TouchDamage : MonoBehaviour
{
    public int damage;
    public bool respawnPlayer = true;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<IHealth>().TakeDamage(damage, respawnPlayer);
        }
    }
}

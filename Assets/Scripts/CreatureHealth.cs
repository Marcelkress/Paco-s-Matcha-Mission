using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class CreatureHealth : MonoBehaviour, IHealth
{
    public int maxHealth = 3;

    [ShowInInspector]private int currentHealth;
    private Animator anim; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (other.transform.position.y > transform.position.y)
            {
                TakeDamage(1, false);
            }
        }
    }

    public void TakeDamage(int amount, bool respawn)
    {
        currentHealth -= amount;
        
        anim.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            anim.SetBool("Dead", true);
        }
    }

    public void Heal(int amount)
    {
        //
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SetRespawnPoint(Transform point)
    {
        // yata yata
    }
    
}

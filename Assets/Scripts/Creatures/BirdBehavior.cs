using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class BirdBehavior : MonoBehaviour
{
    public float catCheckRadius = 5;
    public LayerMask playerLayer;
    
    public float flySpeed = 5;
    private float timePassedSinceFlight;

    public Transform[] flyTargets;
    private int targetIndex = 0;

    public AudioClip[] crowSounds;
    
    private Animator anim;
    private SpriteRenderer sprite;
    private bool isFlying;

    private void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();    
    }

    private void Update()
    {
        if (isFlying == false)
        {
            Collider2D hit = Physics2D.OverlapCircle(transform.position, catCheckRadius, playerLayer);
            
            if (hit != null)
            {
                isFlying = true;
                anim.SetBool("isFlying", true);
                
                TriggerSound sound = GetComponent<TriggerSound>();
                if (sound != null)
                {
                    sound.PlayClip(crowSounds[Random.Range(0, crowSounds.Length)]);
                }
                else
                {
                    Debug.LogError("No TriggerSound script");
                }
            }
            else
            {
                return;
            }
        }

        float lastXPos = transform.position.x;
        
        if (Vector2.Distance(transform.position, flyTargets[targetIndex].position) < 0.1f)
        {
            targetIndex = Random.Range(0, flyTargets.Length);
            
            isFlying = false;
            anim.SetBool("isFlying", false);
        }

        transform.position = Vector2.MoveTowards(transform.position, flyTargets[targetIndex].position,
            flySpeed * Time.deltaTime);
        
        if (lastXPos < transform.position.x)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IHealth health = other.transform.GetComponent<IHealth>();

        if (health != null)
        {
            health.Heal(1);
            Destroy(this.gameObject);
        }
    }
}

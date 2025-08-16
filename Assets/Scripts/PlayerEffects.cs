using System;
using Unity.Mathematics;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [Header("Dust land")]
    public float dustRaycastOffset = 0.5f;
    public float dustRaycastLength = .5f;
    public GameObject dustLandEffect;
    private bool wasInAir;

    [Header("Dust Sprint")] 
    public GameObject sprintDust;
    public Vector2 spawnOffset;

    [Header("Camera shake")] 
    public float shakeIntensity;
    public float shakeTime;

    private bool wasSprinting;
    
    
    private Movement movement;
    private CatInput input;


    void Start()
    {
        wasInAir = false;
        movement = GetComponent<Movement>();
        input = GetComponent<CatInput>();
    }
    
    void LateUpdate()
    {
        LandEffect();
        SprintStartDust();
    }

    private void SprintStartDust()
    {
        if (input.sprint && !wasSprinting && input.inputVector.x != 0)
        {
            GameObject dust = Instantiate(sprintDust, new(transform.position.x + spawnOffset.x, transform.position.y + spawnOffset.y), sprintDust.transform.rotation);
            dust.transform.SetParent(null);

            bool lookingLeft = Mathf.Sign(input.inputVector.x) == 1 ? true : false;

            Vector3 scale = new Vector3();
            scale.x = dust.transform.localScale.x * (lookingLeft ? 1 : -1);
            scale.y = dust.transform.localScale.y;

            dust.transform.localScale = scale;
        }
        wasSprinting = input.sprint;
    }

    private void LandEffect()
    {
        bool isInAir = !movement.collisions.below && !input.wallSliding;
        
        // Detect landing
        if (wasInAir && !isInAir)
        {
            //CameraShake.instance.Shake(shakeIntensity, shakeTime);
            
            RaycastHit2D hit = Physics2D.Raycast( new(transform.position.x,transform.position.y - dustRaycastOffset), Vector2.down, dustRaycastLength);
            if (hit)
            {
                //if (hit.transform.CompareTag("Dirt"))
                //{
                    Instantiate(dustLandEffect, transform.position, quaternion.identity);
                //}
            }
        }
        wasInAir = isInAir;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new(transform.position.x,transform.position.y - dustRaycastOffset), new(transform.position.x,transform.position.y - dustRaycastLength));
    }
}

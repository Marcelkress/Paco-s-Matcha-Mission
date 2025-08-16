using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MatchaMagic : MonoBehaviour
{
    // 1. Instantiate platform beneath player
    // 2. Keep ref to platform
    // 3. Destroy after 2-ish seconds or when a new is instantiated

    public GameObject matchaPlatformPrefab;
    public Vector2 platformSpawnOffset;

    public float platformDestroyTime;
    private float platformDestroyTimer;
    
    public float coolDownTime;
    private float coolDownTimer;

    public Slider sliderUI;

    private GameObject lastPlatform, newPlatform;

    public bool unlocked = false;

    void Start()
    {
        sliderUI.maxValue = coolDownTime;
    }
    
    void Update()
    {
        coolDownTimer += Time.deltaTime;
        sliderUI.value = coolDownTimer;
        
        platformDestroyTimer += Time.deltaTime;

        if (platformDestroyTimer >= platformDestroyTime)
        {
            Destroy(lastPlatform);
        }
    }
    
    public void OnUseMagic(InputValue value)
    {
        if (unlocked == false)
            return;
        
        if (value.isPressed && coolDownTimer >= coolDownTime)
        {
            coolDownTimer = 0;
            platformDestroyTimer = 0;
            
            newPlatform = Instantiate(matchaPlatformPrefab, new Vector3(transform.position.x, transform.position.y - platformSpawnOffset.y, 0), 
                quaternion.identity);
            
            if(lastPlatform != null) 
                Destroy(lastPlatform);

            lastPlatform = newPlatform;
            
            
        }
    }
    
    
}

using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private float startPos;
    private float camStartPos;
    public float parallaxEffect;
    
    public GameObject cam;
    
    void Start()
    {
        // Store the initial position of the object
        startPos = transform.position.x;
        camStartPos = cam.transform.position.x;
        
    }

    void LateUpdate()
    {
        // Calculate how far the camera has moved from its starting position
        float camDelta = cam.transform.position.x - camStartPos;
        
        // Apply parallax effect based on camera movement
        float distance = camDelta * parallaxEffect;

        // Update the object's position
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
    }
}
using System;
using UnityEngine;

public class RespawnObject : MonoBehaviour
{
    public Transform respawnPoint;
    public LayerMask respawnCollisionLayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & respawnCollisionLayer) != 0)
        {
            transform.position = respawnPoint.position;
        }
    }
}

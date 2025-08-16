using System;
using DG.Tweening;
using UnityEngine;

public class JumpInCup : MonoBehaviour
{
    [HideInInspector]public Transform[] targets;
    private int targetIndex = 0;
    public float moveDuration = 0.5f;

    private void Start()
    {
        targetIndex = 0;
    }
    

    // Update is called once per frame
    void Update()
    {
        transform.DOMove(targets[targetIndex].position, moveDuration, true);

        if (Vector2.Distance(transform.position, targets[targetIndex].position) < 0.01) // if its at the target
        {
            targetIndex++;
        }
    }
}

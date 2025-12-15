using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class MoveCameraDown : MonoBehaviour
{
    public Transform cameraTarget;
    public float cameraMoveTime, cameraMoveDistance;

    private CatInput input;
    private bool control;
    
    private void Start()
    {
        input = GetComponent<CatInput>();
    }

    public void Update()
    {
        if (input.crouch && !control)
        {
            control = true;
            MoveDown();
        }
        else if (control == true) 
        {
            control = false;
            MoveUp();
        }
    }

    void MoveDown()
    {
        float endVal = transform.position.y - cameraMoveDistance;
        cameraTarget.DOLocalMoveY(endVal, cameraMoveTime);
    }

    void MoveUp()
    {
        float endVal = 0;
        cameraTarget.DOLocalMoveY(endVal, cameraMoveTime);
    }
}

using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public PlayerInput playerInput;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public void FindObjects()
    {
        playerInput = FindFirstObjectByType<PlayerInput>();
    }
    
    public void ChangeInputActionMap(bool UI)
    {
        if (UI)
        {
            playerInput.SwitchCurrentActionMap("UI");
            //Debug.Log("UI Action map");
        }
        else
        {
            playerInput.SwitchCurrentActionMap("Player");
            //Debug.Log("Player action map");
        }
    }
    
    
    
}

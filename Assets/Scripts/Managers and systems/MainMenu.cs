using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [Header("Buttons for selection")] 
    public GameObject startButton;
    
    private bool gameStarted;

    void Start()
    {
        gameStarted = false;
        EventSystem.current.SetSelectedGameObject(startButton);
    }
    
    public void StartGame()
    {
        if (gameStarted)
            return;
        
        gameStarted = true;
        SceneManager.instance.ChangeScene();
    }
    
    public void QuitGame()
    {
        Debug.Log("QUIT GAME");
        Application.Quit();
    }
}
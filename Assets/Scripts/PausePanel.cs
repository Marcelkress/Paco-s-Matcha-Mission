using System;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [Title("Pause Panel")] public RectTransform pausePanel;
    public Image pauseFadeImage;
    public float fadeValue;
    
    [Title("UI Tabs")]
    public GameObject[] tabs;

    [Title("UI positions")] public RectTransform centerPosition;
    public RectTransform offScreenPosition;

    public float UIAnimationSpeed = 0.2f;
    
    [Title("References")]
    public PlayerInput playerInput;
    public GameObject[] tabNavigationStartButtons;

    [Title("Sounds")] public AudioClip tabSwitchSound;
    public AudioClip buttonClickSound, pauseSound, unpauseSound;

    [Title("Other")] public bool canPause;
    
    
    private bool paused;
    private TriggerSound sound;
    private InputAction RBAction;
    private InputAction LBAction;
    private InputAction pauseAction, unPauseAction;

    private int currentTabIndex = 1;
    
    private void Start()
    {
        sound = GetComponent<TriggerSound>();
        pausePanel.DOAnchorPos(offScreenPosition.anchoredPosition, 0f, true);
        pauseFadeImage.DOFade(0, UIAnimationSpeed);

        RBAction = playerInput.actions["RB"];
        RBAction.performed += NextTab;
        
        LBAction = playerInput.actions["LB"];
        LBAction.performed += PrevTab;

        pauseAction = playerInput.actions["Pause"];
        pauseAction.performed += TogglePause;

        unPauseAction = playerInput.actions["UnPause"];
        unPauseAction.performed += TogglePause;
    }

    public void TogglePause(InputAction.CallbackContext contx)
    {
        TogglePause();
    }

    public void TogglePause()
    {
        if (!canPause)
            return;
        
        if (!paused)
        {
            OpenPausePanel();
            GameManager.instance.ChangeInputActionMap(true);
            paused = true;
        }
        else
        {
            ClosePausePanel();
            GameManager.instance.ChangeInputActionMap(false);
            paused = false;
        }
    }

    private void OpenPausePanel()
    {
        pausePanel.DOAnchorPos(centerPosition.anchoredPosition, UIAnimationSpeed).SetUpdate(true);
        pauseFadeImage.DOFade(fadeValue, UIAnimationSpeed);
        
        currentTabIndex = 1;
        
        foreach (GameObject tab in tabs)
        {
            tab.SetActive(false);
        }

        tabs[0].SetActive(true);
        
        EventSystem.current.SetSelectedGameObject(tabNavigationStartButtons[0]);
        
        if (sound != null)
        {
            sound.PlayClip(pauseSound);
        }
        else
        {
            Debug.LogError("No TriggerSound script");
        }
    }
    private void ClosePausePanel()
    {
        pausePanel.DOAnchorPos(offScreenPosition.anchoredPosition, UIAnimationSpeed).SetUpdate(true);
        pauseFadeImage.DOFade(0, UIAnimationSpeed);
        
        EventSystem.current.SetSelectedGameObject(null);
        
        if (sound != null)
        {
            sound.PlayClip(unpauseSound);
        }
        else
        {
            Debug.LogError("No TriggerSound script");
        }
    }
    
    public void OpenTab(int tabIndex)
    {
        currentTabIndex = tabIndex;
        
        foreach (GameObject tab in tabs)
        {
            tab.SetActive(false);
        }

        tabs[tabIndex - 1].SetActive(true);
        
        EventSystem.current.SetSelectedGameObject(tabNavigationStartButtons[tabIndex - 1]);
        
        if (sound != null)
        {
            sound.PlayClip(tabSwitchSound);
        }
        else
        {
            Debug.LogError("No TriggerSound script");
        }
    }

    private void NextTab(InputAction.CallbackContext context)
    {
        currentTabIndex++;

        if (currentTabIndex > tabs.Length)
        {
            currentTabIndex = 1;
        }
        
        OpenTab(currentTabIndex);
    }

    public void MainMenu()
    {
        SceneManager.instance.ChangeScene(0);
    }

    public void NextTrack()
    {
        MusicManager.instance.NextTrack();
    }

    public void PrevTrack()
    {
        MusicManager.instance.PrevTrack();
    }

    private void PrevTab(InputAction.CallbackContext context)
    {
        currentTabIndex--;

        if (currentTabIndex < 1)
        {
            currentTabIndex = 4;
        }
        
        OpenTab(currentTabIndex);
    }

    public void ButtonSound()
    {
        if (sound != null)
        {
            sound.PlayClip(buttonClickSound);
        }
        else
        {
            Debug.LogError("No TriggerSound script");
        }
    }
}

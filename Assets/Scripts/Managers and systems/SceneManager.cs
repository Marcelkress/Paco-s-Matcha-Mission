using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;
    private int currentSceneIndex;

    public float imgFadeTime;
    public Image sceneFadeImage;

    public UnityEvent loadSceneEvent;
    public UnityEvent sceneLoadedEvent;

    void Awake()
    {
        currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        
        DontDestroyOnLoad(this);
        
    }

    /// <summary>
    /// Loads the scene with the given build index
    /// </summary>
    /// <param name="index"></param>
    public void ChangeScene(int index)
    {
        currentSceneIndex = index;
        StartCoroutine(LoadScene(currentSceneIndex));
    }

    /// <summary>
    /// Loads the next scene in the build index
    /// </summary>
    public void ChangeScene()
    {
        currentSceneIndex++;
        StartCoroutine(LoadScene(currentSceneIndex));
    }

    public void ReloadScene()
    {
        StartCoroutine(LoadScene(currentSceneIndex));
        loadSceneEvent.Invoke();
    }

    private IEnumerator LoadScene(int sceneIndex)
    {
        // Fade screen to black
        sceneFadeImage.DOFade(1, imgFadeTime);
        loadSceneEvent.Invoke();
        
        // Wait for the fade time + 1 seconds
        yield return new WaitForSeconds(imgFadeTime + 1);

        // Load the new scene
        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneIndex);

        // Wait until the scene is fully loaded
        while (asyncLoad.isDone == false)
        {
            yield return null;
        }

        sceneLoadedEvent.Invoke();
        
        // Fade back into game
        sceneFadeImage.DOFade(0, imgFadeTime);
    }

    public int GetCurrentSceneIndex()
    {
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
    }

}

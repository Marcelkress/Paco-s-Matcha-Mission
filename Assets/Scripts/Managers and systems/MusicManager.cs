using DG.Tweening;
using UnityEngine;
using Sirenix.OdinInspector;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    [Title("Tracks")] public AudioClip[] tracks; // Make sure this matches the build indexes
    [ReadOnly]public int trackIndex;

    public float audioFadeTime = 2f, targetVolume = 0.8f;
    
    [HideInInspector] public AudioSource audioSource;

    public static MusicManager instance;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        
        SceneManager.instance.loadSceneEvent.AddListener(StopMusic);
        SceneManager.instance.sceneLoadedEvent.AddListener(StartMusic);

        audioSource = GetComponent<AudioSource>();

        trackIndex = SceneManager.instance.GetCurrentSceneIndex();
        
        audioSource.clip = tracks[trackIndex];
        audioSource.volume = targetVolume;
        audioSource.Play();
    }

    private void StopMusic()
    {
        audioSource.DOFade(0, audioFadeTime);
    }

    private void StartMusic()
    {
        NextTrack();
        audioSource.DOFade(targetVolume, audioFadeTime);
    }

    public void NextTrack()
    {
        trackIndex++;
        if (trackIndex > tracks.Length - 1)
        {
            trackIndex = 0;
        }
        audioSource.clip = tracks[trackIndex];
        audioSource.Play();
        audioSource.volume = targetVolume;
    }

    public void PrevTrack()
    {
        trackIndex--;
        if (trackIndex < 0)
        {
            trackIndex = tracks.Length - 1;
        }
        audioSource.clip = tracks[trackIndex];
        audioSource.Play();
        audioSource.volume = targetVolume;
    }
}

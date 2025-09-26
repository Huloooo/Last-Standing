using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Header("Music Settings")]
    public AudioClip backgroundMusic;   // Normal gameplay music
    public AudioClip bossMusic;         // Special boss music
    [Range(0f, 1f)] public float volume = 0.3f; // Inspector slider

    private AudioSource audioSource;
    private static MusicManager instance;

    void Awake()
    {
        // Singleton: only one MusicManager across scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = volume;
    }

    void Start()
    {
        if (backgroundMusic != null)
        {
            ChangeMusic(backgroundMusic);
        }
    }

    void Update()
    {
        // Live volume control from Inspector
        audioSource.volume = volume;
    }

    public void ChangeMusic(AudioClip newTrack)
    {
        if (newTrack != null && audioSource.clip != newTrack)
        {
            audioSource.clip = newTrack;
            audioSource.Play();
        }
    }

    // Call this when boss spawns
    public void PlayBossMusic()
    {
        ChangeMusic(bossMusic);
    }
}

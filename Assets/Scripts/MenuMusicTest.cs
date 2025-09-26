using UnityEngine;

public class MenuMusicTest : MonoBehaviour
{
    public AudioClip menuMusic;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = menuMusic;
        audioSource.loop = true;
        audioSource.playOnAwake = true;
        audioSource.volume = 0.5f;
        audioSource.Play();
    }
}

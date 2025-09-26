using UnityEngine;

public class MonsterAudio : MonoBehaviour
{
    public AudioClip proximitySound;  // growl, roar, etc.
    public float triggerDistance = 10f;
    public float cooldown = 5f;       // delay before next growl

    private Transform player;
    private AudioSource audioSource;
    private float lastPlayTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= triggerDistance && Time.time - lastPlayTime >= cooldown)
        {
            PlayProximitySound();
            lastPlayTime = Time.time;
        }
    }

    void PlayProximitySound()
    {
        if (proximitySound != null && audioSource != null)
        {
            audioSource.PlayOneShot(proximitySound);
        }
    }
}

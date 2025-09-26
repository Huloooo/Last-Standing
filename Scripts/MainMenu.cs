using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI; // Canvas
    public GameObject player;     // Player object
    private AudioSource menuMusic;

    void Start()
    {
        menuMusic = GetComponent<AudioSource>();
    }

    public void StartGame()
    {
        // Hide menu
        mainMenuUI.SetActive(false);

        // Enable Player
        if (player != null)
        {
            player.SetActive(true);
        }

        // Stop menu music
        if (menuMusic != null)
        {
            menuMusic.Stop();
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}

using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI; // assign Canvas in Inspector

    public void PlayGame()
    {
        Debug.Log("Play button clicked");

        // Disable the whole UI
        if (mainMenuUI != null)
            mainMenuUI.SetActive(false);

        // Enable Player movement
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            PlayerMovementScript movement = player.GetComponent<PlayerMovementScript>();
            if (movement != null)
            {
                movement.enabled = true;
            }
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit button clicked");
        Application.Quit();
    }
}

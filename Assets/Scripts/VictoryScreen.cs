using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private string mainMenuSceneName = "MainMenu";
    
    private void Start()
    {
        // Initially hide the victory screen
        gameObject.SetActive(false);
        
        // Add button listeners
        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);
            
        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(ReturnToMainMenu);
    }
    
    public void RestartGame()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void ReturnToMainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
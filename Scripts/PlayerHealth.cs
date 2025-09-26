using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("UI")]
    public Slider healthBar;
    public GameObject deathScreen;          // Panel with "YOU DIED"
    public CanvasGroup deathScreenCanvas;   // CanvasGroup on DeathScreen
    public GameObject winScreen;            // Panel with "YOU WON"
    public CanvasGroup winScreenCanvas;     // CanvasGroup on WinScreen
    public GameObject crosshair;            // crosshair object
    public GameObject damageFlash;          // red panel (UI Image)

    [Header("Gameplay")]
    public MonoBehaviour[] scriptsToDisable; // movement/shooting scripts to disable on death/win
    public float fadeSpeed = 2f;            // UI fade speed
    public float restartDelay = 3f;         // seconds before auto-restart/return

    private bool isDead = false;
    private bool hasWon = false;
    private float timer = 0f;

    // 🔴 Damage flash internals
    private Image damageFlashImage;
    private bool isFlashing = false;
    private float flashDuration = 0.3f;
    private float flashT = 0f;

    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        if (deathScreen) deathScreen.SetActive(false);
        if (deathScreenCanvas) deathScreenCanvas.alpha = 0f;

        if (winScreen) winScreen.SetActive(false);
        if (winScreenCanvas) winScreenCanvas.alpha = 0f;

        if (damageFlash)
        {
            damageFlashImage = damageFlash.GetComponent<Image>();
            damageFlash.SetActive(false); // start hidden
        }
    }

    void Update()
    {
        // Debug kill player
        if (Input.GetKeyDown(KeyCode.K) && !isDead && !hasWon)
        {
            TakeDamage(currentHealth);
        }

        // Debug win
        if (Input.GetKeyDown(KeyCode.L) && !isDead && !hasWon)
        {
            Win();
        }

        // Handle death fade
        if (isDead)
        {
            if (deathScreenCanvas != null)
                deathScreenCanvas.alpha += Time.unscaledDeltaTime * fadeSpeed;

            timer += Time.unscaledDeltaTime;
            if (timer >= restartDelay)
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        // Handle win fade
        if (hasWon)
        {
            if (winScreenCanvas != null)
                winScreenCanvas.alpha += Time.unscaledDeltaTime * fadeSpeed;

            timer += Time.unscaledDeltaTime;
            if (timer >= restartDelay)
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene("MainMenu");
            }
        }

        // 🔴 Handle damage flash fade
        if (isFlashing && damageFlashImage != null)
        {
            flashT += Time.unscaledDeltaTime / flashDuration;
            float alpha = Mathf.Lerp(0.6f, 0f, flashT);
            damageFlashImage.color = new Color(1f, 0f, 0f, alpha);

            if (flashT >= 1f)
            {
                isFlashing = false;
                damageFlash.SetActive(false);
            }
        }
    }

    public void TakeDamage(int dmg)
    {
        if (isDead || hasWon) return;

        currentHealth -= dmg;
        if (healthBar) healthBar.value = currentHealth;

        // 🔴 Trigger flash
        if (damageFlashImage)
        {
            damageFlash.SetActive(true);
            damageFlashImage.color = new Color(1f, 0f, 0f, 0.6f);
            isFlashing = true;
            flashT = 0f;
        }

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        isDead = true;
        timer = 0f;
        if (deathScreen) deathScreen.SetActive(true);

        foreach (var s in scriptsToDisable) if (s) s.enabled = false;
        if (crosshair) crosshair.SetActive(false);

        Time.timeScale = 0f;
        Debug.Log("YOU DIED triggered");
    }

    public void Win()
    {
        if (isDead || hasWon) return;

        hasWon = true;
        timer = 0f;

        if (winScreen != null)
        {
            winScreen.SetActive(true);
            Debug.Log("WinScreen activated");
        }

        if (winScreenCanvas != null)
        {
            winScreenCanvas.alpha = 0f;
            Debug.Log("WinScreen CanvasGroup found");
        }
        else
        {
            Debug.LogError("WinScreen CanvasGroup is NULL!");
        }

        foreach (var s in scriptsToDisable) if (s != null) s.enabled = false;
        if (crosshair != null) crosshair.SetActive(false);

        Time.timeScale = 0f;
        Debug.Log("YOU WON triggered");
    }
}

using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 200;
    private int currentHealth;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log("Boss took damage, current HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log("Boss defeated! Entering Die()");

        // ✅ Try to find player
        PlayerHealth player = FindObjectOfType<PlayerHealth>();

        if (player != null)
        {
            Debug.Log("Found PlayerHealth, calling Win()");
            player.Win();
        }
        else
        {
            Debug.LogError("No PlayerHealth found in scene!");
        }

        Destroy(gameObject, 1f); // optional delay
    }
}

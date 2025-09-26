using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float health = 100f;
    public float damage = 10f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;

    [Header("Movement")]
    public float moveSpeed = 3f;
    public float detectionRange = 15f;

    private Transform player;
    private float lastAttackTime;
    private bool isDead = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        lastAttackTime = -attackCooldown;
    }

    void Update()
    {
        if (isDead || player == null)
            return;

        float distance = Vector3.Distance(transform.position, player.position);

        // Move towards player if in detection range
        if (distance <= detectionRange && distance > attackRange)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            // Optional: face the player
            transform.LookAt(player);
        }

        // Attack if in range
        if (distance <= attackRange)
        {
            AttemptAttack();
        }
    }

    void AttemptAttack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Debug.Log(gameObject.name + " attacks player for " + damage + " damage");
            // Call player.TakeDamage(damage) if you have player health
            lastAttackTime = Time.time;
        }
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;
        health -= amount;
        if (health <= 0) Die();
    }

    void Die()
    {
        isDead = true;
        Debug.Log(gameObject.name + " died");
        Destroy(gameObject, 2f); // remove enemy after 2 sec
    }
}

using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    public int damage = 20;                  // How much damage the monster deals
    public float attackRange = 2f;           // Distance at which monster can hit
    public float attackCooldown = 1.5f;      // Time between attacks

    private Transform player;
    private float lastAttackTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Call this from Animation Event
    public void DealDamage()
    {
        if (Time.time - lastAttackTime < attackCooldown) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            // Assuming player has a health script
            PlayerHealth ph = player.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage(damage);
                Debug.Log("Monster hit the player for " + damage);
            }

            lastAttackTime = Time.time;
        }
    }
}

using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage = 20;
    public float damageCooldown = 1f;

    private float lastHitTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TryDamage(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TryDamage(other.gameObject);
        }
    }

    void TryDamage(GameObject player)
    {
        if (Time.time >= lastHitTime + damageCooldown)
        {
            PlayerHealth health = player.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
                lastHitTime = Time.time;
                Debug.Log("Enemy damaged player: -" + damage + " HP");
            }
        }
    }
}

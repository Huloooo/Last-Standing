using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    private Collider hitbox;

    void Awake()
    {
        hitbox = GetComponent<Collider>();
        hitbox.enabled = false; // start disabled
    }

    // Called by animation event
    public void EnableHitbox()
    {
        hitbox.enabled = true;
    }

    // Called by animation event
    public void DisableHitbox()
    {
        hitbox.enabled = false;
    }
}

using UnityEngine;

public class GunRaycast : MonoBehaviour
{
    [Header("Gun Settings")]
    public float range = 100f;           // How far the gun can shoot
    public int damage = 25;              // Damage per shot
    public float fireRate = 0.25f;       // Time between shots

    [Header("References")]
    public Transform muzzlePoint;        // Gun muzzle (for muzzle flash)
    public ParticleSystem muzzleFlash;   // Muzzle flash effect
    public GameObject hitEffectPrefab;   // Effect when hitting enemy or wall

    private float nextTimeToFire = 0f;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        if (muzzleFlash != null)
            muzzleFlash.Play();

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range))
        {
            Debug.Log("Hit: " + hit.transform.name);

            // 🟢 Boss
            BossHealth boss = hit.transform.GetComponent<BossHealth>();
            if (boss != null)
            {
                boss.TakeDamage(damage);
                Debug.Log("Boss hit! Damage applied: " + damage);
            }

            // 🟢 Normal enemies
            EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log("Enemy hit! Damage applied: " + damage);
            }

            // 🟢 (Optional) Player - if you want friendly fire
            PlayerHealth player = hit.transform.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
                Debug.Log("Player hit! Damage applied: " + damage);
            }

            // Spawn impact effect
            if (hitEffectPrefab != null)
            {
                Instantiate(hitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }
}

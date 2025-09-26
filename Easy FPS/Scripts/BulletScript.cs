using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{
    [Tooltip("Furthest distance bullet will look for target")]
    public float maxDistance = 1000000;
    RaycastHit hit;

    [Tooltip("Prefab of wall damage hit. The object needs 'LevelPart' tag to create decal on it.")]
    public GameObject decalHitWall;

    [Tooltip("Decal will need to be slightly in front of the wall so it doesnt cause rendering problems.")]
    public float floatInfrontOfWall = 0.05f;

    [Tooltip("Blood prefab particle this bullet will create upon hitting enemy")]
    public GameObject bloodEffect;

    [Tooltip("Put Weapon layer and Player layer to ignore bullet raycast.")]
    public LayerMask ignoreLayer;

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, ~ignoreLayer))
        {
            // Hit environment
            if (hit.transform.CompareTag("LevelPart"))
            {
                Instantiate(decalHitWall, hit.point + hit.normal * floatInfrontOfWall, Quaternion.LookRotation(hit.normal));
                Destroy(gameObject);
            }

            // Hit enemy
            if (hit.transform.CompareTag("Enemy"))
            {
                // Spawn blood FX
                if (bloodEffect)
                {
                    Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                }

                // Apply damage
                EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    enemy.TakeDamage(25); // change damage value as needed
                }

                Destroy(gameObject);
            }

            // Remove bullet after hitting anything
            Destroy(gameObject);
        }

        // Safety destroy (just in case bullet flies forever)
        Destroy(gameObject, 0.1f);
    }
}

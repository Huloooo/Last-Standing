using UnityEngine;
using System.Collections.Generic;

public class EnemySpawnManager : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject[] enemyPrefabs;   // Normal enemies
    public GameObject bossPrefab;       // Final boss

    [Header("Spawn Settings")]
    public Transform[] spawnPoints;     // Spawn points
    public float spawnInterval = 3f;    // Time between spawns
    public float bossSpawnTime = 240f;  // Time (in seconds) before boss spawns
    public int maxEnemiesAlive = 15;    // Cap on how many enemies can be in scene

    private float timer = 0f;
    private bool bossSpawned = false;
    private List<GameObject> activeEnemies = new List<GameObject>();

    void Update()
    {
        timer += Time.deltaTime;

        if (!bossSpawned)
        {
            // Spawn enemies at intervals until boss time
            if (timer >= spawnInterval && Time.time < bossSpawnTime)
            {
                if (activeEnemies.Count < maxEnemiesAlive)
                {
                    SpawnRandomEnemy();
                }
                timer = 0f;
            }

            // Spawn boss after time and clear enemies
            if (Time.time >= bossSpawnTime)
            {
                SpawnBoss();
                ClearAllEnemies();
                bossSpawned = true;
            }
        }

        // Clean up null entries if enemies are destroyed
        activeEnemies.RemoveAll(item => item == null);
    }

    void SpawnRandomEnemy()
    {
        if (enemyPrefabs.Length == 0 || spawnPoints.Length <= 1) return;

        // Pick random enemy and random spawn point (excluding spawn point 0)
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Transform spawnPoint = spawnPoints[Random.Range(1, spawnPoints.Length)];

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        activeEnemies.Add(newEnemy);
    }

    void SpawnBoss()
    {
        if (bossPrefab == null || spawnPoints.Length == 0) return;

        // Always spawn boss at Element 0
        Transform bossSpawnPoint = spawnPoints[0];
        Instantiate(bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);

        // 🔥 Trigger boss music
        MusicManager mm = FindObjectOfType<MusicManager>();
        if (mm != null)
        {
            mm.PlayBossMusic();
            Debug.Log("Boss music triggered!");
        }

        Debug.Log("Final Boss Spawned!");
    }

    void ClearAllEnemies()
    {
        foreach (GameObject enemy in activeEnemies)
        {
            if (enemy != null)
                Destroy(enemy);
        }
        activeEnemies.Clear();
    }
}

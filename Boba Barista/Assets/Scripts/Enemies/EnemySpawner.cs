using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject[] enemyPrefabs; // ✅ Now an array!
    public Transform[] spawnPoints;

    [Header("Spawning Control")]
    public float spawnInterval = 3f;
    public int maxEnemies = 10;
    public bool autoStart = true;

    private int currentEnemyCount = 0;
    private bool isSpawning = false;

    private void Start()
    {
        if (autoStart)
            StartSpawning();
    }

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
        }
    }

    public void StopSpawning()
    {
        if (isSpawning)
        {
            isSpawning = false;
            CancelInvoke(nameof(SpawnEnemy));
        }
    }

    public void SetSpawnInterval(float newInterval)
    {
        spawnInterval = newInterval;

        if (isSpawning)
        {
            StopSpawning();
            StartSpawning();
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0 || spawnPoints.Length == 0) return;
        if (currentEnemyCount >= maxEnemies) return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject prefabToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        
        GameObject enemy = Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
        currentEnemyCount++;
    }

    public void NotifyEnemyDestroyed()
    {
        currentEnemyCount = Mathf.Max(0, currentEnemyCount - 1);
    }
}

using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;

    [Header("Spawning Control")]
    public float spawnIntervalBetweenWaves = 5f; // Time between full spawn waves
    public float intervalBetweenPoints = 1.5f; // Delay between each spawn point
    [Range(0f, 1f)] public float[] spawnChances; // Each spawn point's chance to spawn
    public int maxEnemies = 10;
    public bool autoStart = true;

    private int currentEnemyCount = 0;
    private bool isSpawning = false;
    private Coroutine spawnRoutine;

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
            spawnRoutine = StartCoroutine(SpawnLoop());
        }
    }

    public void StopSpawning()
    {
        if (isSpawning)
        {
            isSpawning = false;
            if (spawnRoutine != null)
                StopCoroutine(spawnRoutine);
        }
    }

    public void SetIntervalBetweenPoints(float newInterval)
    {
        intervalBetweenPoints = newInterval;
    }

    public void SetWaveInterval(float newInterval)
    {
        spawnIntervalBetweenWaves = newInterval;
    }

    IEnumerator SpawnLoop()
    {
        while (isSpawning)
        {
            yield return StartCoroutine(SpawnOnEachPoint());
            yield return new WaitForSeconds(spawnIntervalBetweenWaves);
        }
    }

    IEnumerator SpawnOnEachPoint()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (enemyPrefabs.Length == 0 || currentEnemyCount >= maxEnemies)
                yield break;

            float chance = (i < spawnChances.Length) ? spawnChances[i] : 1f;

            if (Random.value <= chance)
            {
                GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
                Transform spawnPoint = spawnPoints[i];

                Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
                currentEnemyCount++;
            }

            yield return new WaitForSeconds(intervalBetweenPoints);
        }
    }

    public void NotifyEnemyDestroyed()
    {
        currentEnemyCount = Mathf.Max(0, currentEnemyCount - 1);
    }
}

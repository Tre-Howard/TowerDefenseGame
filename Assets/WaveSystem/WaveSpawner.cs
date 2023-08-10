using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// end goal: have a wavespawner system that count wave's on the ui, activates spawners per wave, and can add other spawners or activate them for later rounds in the level

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public float timeBetweenWaves = 5f;
    private int currentWaveIndex = 0;
    private float countdownTimer = 5f;
    private bool isSpawning = true;

    private void Update()
    {
        if (isSpawning && currentWaveIndex < waves.Length)
        {
            if (countdownTimer <= 0f)
            {
                StartCoroutine(SpawnWave());
                countdownTimer = timeBetweenWaves;
            }
            countdownTimer -= Time.deltaTime;
        }
    }

    private IEnumerator SpawnWave()
    {
        Wave currentWave = waves[currentWaveIndex];
        foreach (Wave.EnemySpawnInfo enemySpawnInfo in currentWave.enemiesToSpawn)
        {
            for (int i = 0; i < enemySpawnInfo.count; i++)
            {
                GameObject enemyPrefab = enemySpawnInfo.enemyPrefab;
                GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
                if (enemyMove != null)
                {
                    enemyMove.waypointSystem = currentWave.waypointSystem;
                }
                yield return new WaitForSeconds(1f / currentWave.spawnRate);
            }
        }

        currentWaveIndex++;

        if (currentWaveIndex >= waves.Length)
        {
            isSpawning = false; // Stop spawning when all waves have been completed
        }
    }
}
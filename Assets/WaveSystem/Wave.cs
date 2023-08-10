using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    [System.Serializable]
    public class EnemySpawnInfo
    {
        public GameObject enemyPrefab;
        public int count;
    }

    public EnemySpawnInfo[] enemiesToSpawn;
    public float spawnRate;
    public WaypointSystem waypointSystem;
}
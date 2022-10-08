using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int maxEnemiesAllowed;
    public float spawnRate = 1f;
    private float nextTimeToSpawn;
    public List<Transform> players = new List<Transform>();
    public float MinX;
    public float MaxX;
    public float MinY;
    public float MaxY;
    public GameObject AI;
    public Health health;
    public Score score;

    private void Update()
    {
        if (players.Count < maxEnemiesAllowed)
        {
            if (Time.time >= nextTimeToSpawn)
            {
                nextTimeToSpawn = Time.time + 1f / spawnRate;

                GameObject spawnedPrefab = GameObject.Instantiate(AI, new Vector2(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY)), Quaternion.identity);
                Ctr ctr = spawnedPrefab.GetComponent<Ctr>();

                players.Add(spawnedPrefab.transform);
                ctr.healthScript = health;
                ctr.scoreScript = score;
                ctr.spawner = this;
            }
        }
    }
}

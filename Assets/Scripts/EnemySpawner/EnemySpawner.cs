using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefab")]
    [SerializeField] private GameObject EnemyPrefab;

    [Header("References")]
    [SerializeField] private Location Location;
    [SerializeField] private MovePoints MovePoints;

    [Header("Spawner Parametres")]
    [SerializeField] private float startTimer;
    [SerializeField] private float spawnInterval;

    [Header("Spawned Enemies")]
    [SerializeField] private Transform spawnedEnemiesTransform;
    [SerializeField] private List<Enemy> spawnedEnemiesList = new List<Enemy>();

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(startTimer);
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        GameObject spawnedEnemy = Instantiate(EnemyPrefab, spawnedEnemiesTransform);
        
        Enemy enemy = spawnedEnemy.GetComponent<Enemy>();
        enemy.Spawn(MovePoints.PointsArray);
        spawnedEnemiesList.Add(enemy);

        StartCoroutine(RepeatSpawnEnemyAfterInterval());
    }

    private IEnumerator RepeatSpawnEnemyAfterInterval()
    {
        yield return new WaitForSeconds(spawnInterval);
        SpawnEnemy();
    }
}
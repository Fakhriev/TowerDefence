using System;
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

    [Header("Enemies To Spawn")]
    [SerializeField] private EnemyData_SO[] enemiesData = new EnemyData_SO[0];
    [SerializeField] private EnemyTypeAndAmountToSpawn[] enemiesToSpawn = new EnemyTypeAndAmountToSpawn[0];

    private List<EnemyData_SO> enemyDataList = new List<EnemyData_SO>();
    private int spawnIndex = 0;
    private int enemiesAmount;

    private IEnumerator Start()
    {
        foreach(EnemyTypeAndAmountToSpawn enemyToSpawn in enemiesToSpawn)
        {
            EnemyData_SO enemyDataSO = Array.Find(enemiesData, enemyData => enemyData.EnemyData.Type == enemyToSpawn.Type);

            for(int i = 0; i < enemyToSpawn.Amount; i++)
            {
                enemyDataList.Add(enemyDataSO);
            }

            enemiesAmount += enemyToSpawn.Amount;
        }

        yield return new WaitForSeconds(startTimer);
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        GameObject spawnedEnemy = Instantiate(EnemyPrefab, spawnedEnemiesTransform);
        
        Enemy enemy = spawnedEnemy.GetComponent<Enemy>();

        EnemyData enemyData = enemyDataList[spawnIndex].EnemyData;
        enemy.Spawn(MovePoints.PointsArray, enemyData);

        spawnedEnemiesList.Add(enemy);

        if (spawnIndex + 1 < enemiesAmount)
        {
            spawnIndex++;
            StartCoroutine(RepeatSpawnEnemyAfterInterval());
        }
        else
        {
            Debug.Log("All Enemies Was Spawned");
        }
    }

    private IEnumerator RepeatSpawnEnemyAfterInterval()
    {
        yield return new WaitForSeconds(spawnInterval);
        SpawnEnemy();
    }
}

[System.Serializable]
public class EnemyTypeAndAmountToSpawn
{
    [SerializeField] private EnemyType type;
    [SerializeField] private int amount;

    public EnemyType Type
    {
        get
        {
            return type;
        }
    }

    public int Amount
    {
        get
        {
            return amount;
        }
    }
}
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

    [Header("Default Spawner Parametres")]
    [SerializeField] private float defaultStartTimer;
    [SerializeField] private float defaultEnemyStartInterval;
    [SerializeField] private float defaultWaveInterval;

    [Header("Enemy Spawn Components")]
    [SerializeField] private Transform spawnedEnemiesTransform;
    [SerializeField] private GameRoundsConfig_SO gameRoundsConfigSO;
    [SerializeField] private EnemyData_SO[] enemiesData = new EnemyData_SO[0];

    private List<Enemy> spawnedEnemiesList = new List<Enemy>();
    private List<EnemyData> aliveEnemiesList = new List<EnemyData>();

    private void Awake()
    {
        SetupEnemiesToSpawn();
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(defaultStartTimer);
        StartEnemy();
    }

    private void SetupEnemiesToSpawn()
    {
        Round round = gameRoundsConfigSO.Rounds[0];

        foreach(Wave wave in round.Waves)
        {
            EnemyData enemyData = Array.Find(enemiesData, enemyDataSO => enemyDataSO.EnemyData.Type == wave.EnemyType).EnemyData;
            //Получить данные врага в Волне

            for(int i = 0; i < wave.Amount; i++)
            {
                GameObject createdEnemy = Instantiate(EnemyPrefab, spawnedEnemiesTransform);

                Enemy enemy = createdEnemy.GetComponent<Enemy>();
                enemy.Spawn(MovePoints.PointsArray, enemyData);

                //Заспавнить врага этого типа столько раз, сколько wave.Amount и добавить их в пулл.
                spawnedEnemiesList.Add(enemy);
            }
        }
    }

    private void StartEnemy()
    {
        spawnedEnemiesList[0].StartMoving();
        aliveEnemiesList.Add(spawnedEnemiesList[0].MyEnemyData);
        spawnedEnemiesList.RemoveAt(0);

        if(spawnedEnemiesList.Count > 0)
        {
            StartCoroutine(RepeatStartEnemyAfterInterval());
        }
        else
        {
            Debug.Log("All Enemies Was Started");
        }
    }

    private IEnumerator RepeatStartEnemyAfterInterval()
    {
        yield return new WaitForSeconds(defaultEnemyStartInterval);
        StartEnemy();
    }
}
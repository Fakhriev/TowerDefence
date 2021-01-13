using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefab")]
    [SerializeField] private GameObject EnemyPrefab;

    [Header("References")]
    [SerializeField] private MovePoints MovePoints;
    [SerializeField] private EnemyMoveStarter EnemyMoveStarter;

    [Header("Enemy Spawn Components")]
    [SerializeField] private Transform spawnedEnemiesTransform;
    [SerializeField] private GameRoundsConfig_SO gameRoundsConfigSO;
    [SerializeField] private EnemyData_SO[] enemiesData = new EnemyData_SO[0];

    private Round currentRound;

    private void Awake()
    {
        if(PlayerPrefs.HasKey("Round") == true)
        {
            //TODO
            currentRound = gameRoundsConfigSO.Rounds[0];
        }
        else
        {
            currentRound = gameRoundsConfigSO.Rounds[0];
        }

        SpawnEnemiesPull();
    }

    private void SpawnEnemiesPull()
    {
        Round round = currentRound;
        List<EnemyWave> enemyWavesList = new List<EnemyWave>();

        for(int i = 0; i < round.Waves.Length; i++)
        {
            EnemyWave enemyWave = new EnemyWave();
            enemyWave.WaveNumber = i;
            List<Enemy> enemiesInWaveList = new List<Enemy>();

            foreach(Microwave microwave in round.Waves[i].Microwaves)
            {
                EnemyType enemyTypeInMicrowave = microwave.EnemyType;
                EnemyData enemyData = Array.Find(enemiesData, enemyDataSO => enemyDataSO.EnemyData.Type == enemyTypeInMicrowave).EnemyData;
                //Получить данные врага в Микроволне

                for (int y = 0; y < microwave.Amount; y++)
                {
                    GameObject createdEnemy = Instantiate(EnemyPrefab, spawnedEnemiesTransform);

                    Enemy enemy = createdEnemy.GetComponent<Enemy>();
                    enemy.Spawn(MovePoints.PointsArray, enemyData);

                    //Заспавнить врага этого типа столько раз, сколько microwave.Amount и добавить их в пулл.
                    enemiesInWaveList.Add(enemy);
                }
            }

            enemyWave.EnemiesInWave = enemiesInWaveList;
            enemyWave.NextWaveStartTimer = round.Waves[i].TimerToNextWave;
            enemyWavesList.Add(enemyWave);
        }

        EnemyMoveStarter.SetEnemyWavesList(enemyWavesList);
    }
}
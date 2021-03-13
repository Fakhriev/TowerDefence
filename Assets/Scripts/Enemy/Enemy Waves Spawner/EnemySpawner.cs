using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefab")]
    [SerializeField] private GameObject EnemyPrefab;

    [Header("References")]
    [SerializeField] private EnemyMoveStarter EnemyMoveStarter;
    [SerializeField] private RoundsController RoundsController;

    [Header("Enemy Spawn Components")]
    [SerializeField] private Transform spawnedEnemiesTransform;
    [SerializeField] private EnemyData_SO[] enemiesData = new EnemyData_SO[0];

    private MovePoints MovePoints;
    private Round currentRound;

    public void PrepareToSpwanEnemyPull(MovePoints MovePoints)
    {
        this.MovePoints = MovePoints;

        currentRound = RoundsController.GetCurrentRound();
        SpawnEnemiesPull();
    }

    public void SpawnEnemiesPull()
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
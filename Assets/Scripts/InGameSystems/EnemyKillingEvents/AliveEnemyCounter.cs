using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliveEnemyCounter : MonoBehaviour
{
    private List<EnemyData> aliveEnemiesList = new List<EnemyData>();

    private void Start()
    {
        EnemyKillEvents.Instance.OnEnemyDie += RemoveAliveEnemyFromList;
    }

    private void RemoveAliveEnemyFromList(EnemyData enemyData)
    {
        aliveEnemiesList.Remove(enemyData);

        if (aliveEnemiesList.Count == 0)
        {
            Debug.Log("All Spawned Enemies Was Killed");
            StartCoroutine(InvokeGameWinAfterTimer());
        }
    }

    private IEnumerator InvokeGameWinAfterTimer()
    {
        yield return new WaitForSeconds(1.5f);
        GameEndEvents.InvokeOnGameEndEvent(GameEndType.Win);
    }

    public void SetAliveEnemiesListFromEnemiesWaves(List<EnemyWave> enemiesWave)
    {
        foreach(EnemyWave wave in enemiesWave)
        {
            foreach(Enemy enemy in wave.EnemiesInWave)
            {
                aliveEnemiesList.Add(enemy.MyEnemyData);
            }
        }
    }
}
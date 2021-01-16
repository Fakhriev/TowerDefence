using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveStarter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AliveEnemyCounter AliveEnemyCounter;
    [SerializeField] private RoundsController RoundsController;

    [Header("Default Enemy Move Starter Parametres")]
    [SerializeField] private float defaultStartTimer;
    [SerializeField] private float defaultEnemyStartInterval;
    [SerializeField] private float defaultWaveInterval;

    private List<EnemyWave> enemyWavesToStartList;

    private int currentWave;

    private void StartWave()
    {
        StartCoroutine(WaveEnemiesStarter());
    }

    private IEnumerator WaveEnemiesStarter()
    {
        List<Enemy> enemiesInWave = enemyWavesToStartList[currentWave].EnemiesInWave;

        while (enemiesInWave.Count > 0)
        {
            enemiesInWave[0].StartMoving();
            enemiesInWave.RemoveAt(0);

            yield return new WaitForSeconds(defaultEnemyStartInterval);
        }

        if (currentWave == enemyWavesToStartList.Count - 1)
        {
            Debug.Log("That was last wave of Enemies To Start. Last Enemy Was Started");
        }
        else
        {
            //float waveInterval = (enemyWavesToStartList[currentWave].NextWaveStartTimer != 0) ? enemyWavesToStartList[currentWave].NextWaveStartTimer : defaultWaveInterval;
            //Set The Default Wave Interval, If Setuped Interval in Scriptable Object Was Zero;

            yield return new WaitForSeconds(enemyWavesToStartList[currentWave].NextWaveStartTimer);
            currentWave++;
            StartWave();
        }
    }

    private IEnumerator StartFirstWaveAfterTimer()
    {
        RoundsController.ShowCurrentRoundInUI();
        yield return new WaitForSeconds(defaultStartTimer);
        StartWave();
    }

    public void SetEnemyWavesList(List<EnemyWave> enemyWavesList)
    {
        enemyWavesToStartList = enemyWavesList;
        AliveEnemyCounter.SetAliveEnemiesListFromEnemiesWaves(enemyWavesList);

        StartCoroutine(StartFirstWaveAfterTimer());
    }
}
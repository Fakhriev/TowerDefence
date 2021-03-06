﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [Header("Waves Thins")]
    [SerializeField] private GameObject waveUIElement;
    [SerializeField] private TextMeshProUGUI tmpWaveNumber;

    private List<EnemyWave> enemyWavesToStartList;

    private int currentWave;

    private void StartWave()
    {
        tmpWaveNumber.text = $"{currentWave + 1}/{enemyWavesToStartList.Count}";
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
        yield return new WaitForSeconds(1);
        RoundsController.ShowCurrentRoundInUI();

        yield return new WaitForSeconds(defaultStartTimer);

        currentWave = 0;
        waveUIElement.SetActive(true);

        StartWave();
    }

    public void SetEnemyWavesList(List<EnemyWave> enemyWavesList)
    {
        enemyWavesToStartList = enemyWavesList;
        AliveEnemyCounter.SetAliveEnemiesListFromEnemiesWaves(enemyWavesList);

        StartCoroutine(StartFirstWaveAfterTimer());
    }
}
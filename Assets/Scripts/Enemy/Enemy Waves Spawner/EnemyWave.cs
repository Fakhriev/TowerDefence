using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyWave
{
    [SerializeField] private int waveNumber;

    [SerializeField] private List<Enemy> enemiesInWave;

    [SerializeField] private float nextWaveStartTimer;

    public int WaveNumber
    {
        get
        {
            return waveNumber;
        }

        set
        {
            waveNumber = value;
        }
    }

    public List<Enemy> EnemiesInWave
    {
        get
        {
            return enemiesInWave;
        }

        set
        {
            enemiesInWave = value;
        }
    }

    public float NextWaveStartTimer
    {
        get
        {
            return nextWaveStartTimer;
        }

        set
        {
            nextWaveStartTimer = value;
        }
    }
}
using System;
using UnityEngine;

[Serializable]
public class Round 
{
    [Header("Waves")]
    [SerializeField] private Wave[] waves;

    public Wave[] Waves
    {
        get
        {
            return waves;
        }
    }
}

[Serializable]
public class Wave
{
    [Header("Enemy Type And Amount In Wave")]
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private int amount;

    [Header("Next Wave Timer")]
    [SerializeField] private float timerToNextWave;

    public EnemyType EnemyType
    {
        get
        {
            return enemyType;
        }
    }

    public int Amount
    {
        get
        {
            return amount;
        }
    }

    public float TimerToNextWave
    {
        get
        {
            return timerToNextWave;
        }
    }
}
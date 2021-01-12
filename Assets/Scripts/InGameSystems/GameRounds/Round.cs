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
    [Header("Micro Waves")]
    [SerializeField] private Microwave[] microwaves;

    [Header("Next Wave Timer")]
    [SerializeField] private float timerToNextWave;

    public Microwave[] Microwaves
    {
        get
        {
            return microwaves;
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

[Serializable]
public class Microwave
{
    [Header("Enemy Type And Amount In Wave")]
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private int amount;

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
}
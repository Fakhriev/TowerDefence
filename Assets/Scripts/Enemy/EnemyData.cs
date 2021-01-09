using System;
using UnityEngine;

[Serializable]
public class EnemyData
{
    [Header("Enemy Type")]
    [SerializeField] private EnemyType type;

    [Header("Enemy Stats")]
    [SerializeField] private EnemyStats enemyStats;

    [Header("Enemy Mesh Prefab")]
    [SerializeField] private GameObject meshPrefab;

    public EnemyType Type
    {
        get
        {
            return type;
        }
    }

    public EnemyStats EnemyStats
    {
        get
        {
            return enemyStats;
        }
    }

    public GameObject MeshPrefab
    {
        get
        {
            return meshPrefab;
        }
    }
}

[Serializable]
public enum EnemyType
{
    Soldier,
    Golem,
    Ork
}

[Serializable]
public class EnemyStats
{
    [SerializeField] private int health;

    [SerializeField] private float speed;
    [SerializeField] private float damagePerSecond;

    [SerializeField] private int goldForKill;

    public int Health
    {
        get
        {
            return health;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }
    }

    public float DamagePerSecond
    {
        get
        {
            return damagePerSecond;
        }
    }

    public int GoldForKill
    {
        get
        {
            return goldForKill;
        }
    }
}
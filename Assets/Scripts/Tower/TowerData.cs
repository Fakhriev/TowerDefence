using System;
using UnityEngine;

[Serializable]
public class TowerData
{
    [Header("Type")]
    [SerializeField] private TowerTypes type;

    [Header("Cost")]
    [SerializeField] private int buildCost;

    [Header("Arrow Prefab")]
    [SerializeField] private GameObject arrowPrefab;

    [Header("Tower Levels")]
    [SerializeField] private TowerOneLevel[] levelsArray;

    public TowerTypes Type
    {
        get
        {
            return type;
        }
    }

    public int BuildCost
    {
        get
        {
            return buildCost;
        }
    }

    public GameObject ArrowPrefab
    {
        get
        {
            return arrowPrefab;
        }
    }

    public TowerOneLevel[] LevelsArray
    {
        get
        {
            return levelsArray;
        }
    }
}

[Serializable]
public enum TowerTypes
{
    Tower1,
    Tower2,
    Tower3,
    Tower4
}

[Serializable]
public class TowerOneLevel
{
    [Header("Tower Level")]
    [Range(1, 5)] [SerializeField] private int level = 1;
    [SerializeField] private GameObject meshPrefab;
    [SerializeField] private int upgradeToNextLevelCost;
    [SerializeField] private TowerStats stats;

    public int Level
    {
        get
        {
            return level;
        }
    }

    public GameObject MeshPrefab
    {
        get
        {
            return meshPrefab;
        }
    }

    public int UpgraeToNextLevelCost
    {
        get
        {
            return upgradeToNextLevelCost;
        }
    }

    public TowerStats Stats
    {
        get
        {
            return stats;
        }
    }
}

[Serializable]
public class TowerStats
{
    [Range(1, 100)] [SerializeField] private int damage = 1;
    [Range(0.1f, 10)] [SerializeField] private float attacksInSecond = 1;
    [Range(1, 15)] [SerializeField] private int range = 1;

    public int Damage
    {
        get
        {
            return damage;
        }
    }

    public float AttacksInSecond
    {
        get
        {
            return attacksInSecond;
        }
    }

    public int Range
    {
        get
        {
            return range;
        }
    }
}
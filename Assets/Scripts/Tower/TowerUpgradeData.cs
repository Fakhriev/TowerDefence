using UnityEngine;

[System.Serializable]
public class TowerUpgradeData
{
    private Tower towerToUpgrade;

    private int upgradeCost;
    private int sellCost;

    private bool isTowerMaxLevel;

    public Tower TowerToUpgrade
    {
        get
        {
            return towerToUpgrade;
        }

        set
        {
            towerToUpgrade = value;
        }
    }

    public int UpgradeCost
    {
        get
        {
            return upgradeCost;
        }

        set
        {
            upgradeCost = value;
        }
    }

    public int SellCost
    {
        get
        {
            return sellCost;
        }

        set
        {
            sellCost = value;
        }
    }

    public bool IsTowerMaxLevel
    {
        get
        {
            return isTowerMaxLevel;
        }

        set
        {
            isTowerMaxLevel = value;
        }

    }
}
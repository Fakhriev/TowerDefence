using System;
using UnityEngine;

public class TowerUpgrade : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Tower Tower;
    [SerializeField] private TowerTargetSystem TowerTargetSystem;
    [SerializeField] private TowerAttack TowerAttack;

    private GameObject towerMeshGO;
    private TowerOneLevel towerLevel;

    private int level;
    private int sellCost;

    private bool isTowerMaxLevel;

    private void OnMouseUp()
    {
        TowerUpgradeData towerUpgradeData = GetTowerToUpgradeData();
        TowerUpgradeEvents.InvokeOnTowerClickEvent(towerUpgradeData);

        //Debug.Log("TowerMenu: OnMouseUp");
    }

    private void SetAllParametres()
    {
        SetupTowerLevel();

        SetupMesh();
        SetupStats();
        CalculateSellCost();

        SetupShootingPosition();
    }

    private void SetupTowerLevel()
    {
        towerLevel = Array.Find(Tower.MyTowerData.LevelsArray, levelInArray => levelInArray.Level == level);
    }

    private void SetupMesh()
    {
        towerMeshGO = Instantiate(towerLevel.MeshPrefab, Tower.TowerComponenets.MeshParent);
    }

    private void SetupStats()
    {
        TowerTargetSystem.SetupRange(towerLevel.Stats.Range);

        TowerAttack.SetAttacksSpeedAndDamage(towerLevel.Stats.AttacksInSecond, towerLevel.Stats.Damage);
        //TowerAttack.SetArrowPrefab(Tower.MyTowerData.ArrowPrefab);
    }

    private void CalculateSellCost()
    {
        if(level == 1)
        {
            sellCost = (int)(Tower.MyTowerData.BuildCost / 2);
        }
        else
        {
            int sellUpgradesCost = 0;

            for(int i = 0; i < level - 1; i++)
            {
                sellUpgradesCost += Tower.MyTowerData.LevelsArray[i].UpgraeToNextLevelCost / 2;
            }

            sellCost = (int)(Tower.MyTowerData.BuildCost / 2) + sellUpgradesCost;
        }

        //Debug.Log($"Build Cost: {Tower.MyTowerData.BuildCost}. Calculated Sell Cost: {sellCost}");
    }

    private void SetupShootingPosition()
    {
        TowerMeshPrefab towerMesh = towerMeshGO.GetComponent<TowerMeshPrefab>();
        TowerAttack.SetShootingPosition(towerMesh.ShootingPosition);
    }

    private bool IsTowerMaxLevel()
    {
        if (Array.Find(Tower.MyTowerData.LevelsArray, levelInArray => levelInArray.Level == (level + 1)) == null)
        {
            //Debug.Log("TowerUpgrade: IsCanBeUpgraded(): Have not this Level in LevelsArray");
            return true;
        }
        else
        {
            return false;
        }
    }

    private TowerUpgradeData GetTowerToUpgradeData()
    {
        TowerUpgradeData value = new TowerUpgradeData();

        value.TowerToUpgrade = Tower;

        value.UpgradeCost = towerLevel.UpgraeToNextLevelCost;
        value.SellCost = sellCost;

        value.IsTowerMaxLevel = isTowerMaxLevel;

        return value;
    }

    public void Upgrade()
    {
        if(isTowerMaxLevel == true)
        {
            Debug.LogWarning($"Tower [{Tower.MyTowerData.Type}] is Maximal Level. It's Can Not Be Upgraded from Level [{level}] to Level [{level + 1}]");
            return;
        }

        level++;
        Destroy(towerMeshGO);

        SetAllParametres();
        isTowerMaxLevel = IsTowerMaxLevel();
    }

    public void SetupTowerLevelOne()
    {
        level = 1;
        isTowerMaxLevel = false;

        SetAllParametres();
    }
}
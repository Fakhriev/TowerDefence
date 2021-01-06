using System;
using UnityEngine;

public class TowerUpgrade : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Tower Tower;
    [SerializeField] private TowerTargetSystem TowerTargetSystem;
    [SerializeField] private TowerAttack TowerAttack;

    private GameObject towerMeshGO;
    private Transform shootingPosition;

    private TowerOneLevel towerLevel;
    private int level;

    private void OnMouseUp()
    {
        TowerUpgradeEvents.InvokeOnTowerClickEvent(Tower);

        //Debug.Log("TowerMenu: OnMouseUp");
    }

    private void SetAllParametres()
    {
        SetupTowerLevel();

        SetupMesh();
        SetupStats();

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

    private void SetupShootingPosition()
    {
        TowerMeshPrefab towerMesh = towerMeshGO.GetComponent<TowerMeshPrefab>();
        TowerAttack.SetShootingPosition(towerMesh.ShootingPosition);
    }

    private bool IsCanBeUpgraded()
    {
        if (Array.Find(Tower.MyTowerData.LevelsArray, levelInArray => levelInArray.Level == (level + 1)) == null)
        {
            //Debug.Log("TowerUpgrade: IsCanBeUpgraded(): Have not this Level in LevelsArray");
            return false;
        }
        else
        {
            return true;
        }
    }

    public void Upgrade()
    {
        if(IsCanBeUpgraded() == false)
        {
            Debug.LogWarning($"Tower [{Tower.MyTowerData.Type}] Can Not Be Upgraded from Level [{level}] to Level [{level + 1}]");
            return;
        }

        level++;
        Destroy(towerMeshGO);

        SetAllParametres();
    }

    public void SetupTowerLevelOne()
    {
        level = 1;

        SetAllParametres();
    }
}
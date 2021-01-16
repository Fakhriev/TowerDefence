using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TowerUpgrade TowerUpgrade;
    [SerializeField] private TowerTargetSystem TowerTargetSystem;
    [SerializeField] private TowerAttack TowerAttack;

    [Header("Tower Components")]
    [SerializeField] private TowerComponents towerComponents;

    private TowerData myTowerData;
    private Foundation myFoundation;

    private void Start()
    {
        GameEndEvents.Instance.OnGameEnd += GameEnd;
    }

    private void GameEnd(GameEndType gameEndType)
    {
        if(gameEndType == GameEndType.Loose)
        {
            StopAttack();
        }
    }

    private void StopAttack()
    {
        TowerTargetSystem.enabled = false;
        TowerTargetSystem.gameObject.SetActive(false);
        TowerAttack.StopShooting();
    }

    private void OnDestroy()
    {
        GameEndEvents.Instance.OnGameEnd -= GameEnd;
    }

    public void SetupTower(TowerData towerData, Foundation foundation)
    {
        myTowerData = towerData;
        myFoundation = foundation;

        towerComponents.DefaultMesh.SetActive(false);
        TowerUpgrade.SetupTowerLevelOne();

        myFoundation.Deactivate();
    }

    public void Upgrade()
    {
        TowerUpgrade.Upgrade();
    }

    public void Sell()
    {
        myFoundation.Activate();
        Destroy(gameObject);
    }

    public TowerComponents TowerComponenets
    {
        get
        {
            return towerComponents;
        }
    }

    public TowerData MyTowerData
    {
        get
        {
            return myTowerData;
        }
    }
}
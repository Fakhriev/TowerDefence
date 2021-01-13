using UnityEngine;

public class TowerUpgradeEvents : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GoldController GoldController;
    [SerializeField] private UIMenusController UIMenusController;

    public static TowerUpgradeEvents Instance { get { return instance; } }
    private static TowerUpgradeEvents instance;

    public delegate void MethodContainerWithTowerUpgradeData(TowerUpgradeData TowerUpgradeData);
    public event MethodContainerWithTowerUpgradeData OnTowerClick;

    public delegate void MethodContainer();
    public event MethodContainer OnTowerUpgrade;
    public event MethodContainer OnTowerSell;

    private TowerUpgradeData towerUpgradeData;

    private void Awake()
    {
        instance = this;

        OnTowerClick += OpenUpgradeMenu;

        OnTowerUpgrade += UpgradeTower;
        OnTowerSell += SellTower;
    }

    private void OpenUpgradeMenu(TowerUpgradeData towerUpgradeData)
    {
        this.towerUpgradeData = towerUpgradeData;
        UIMenusController.OpenUpgradeMenu(towerUpgradeData.TowerToUpgrade.transform.position, towerUpgradeData.UpgradeCost, towerUpgradeData.IsTowerMaxLevel);
        
        //Debug.Log("TowerUpgradeEvents: OpenUpgradeMenu");
    }

    private void UpgradeTower()
    {
        towerUpgradeData.TowerToUpgrade.Upgrade();
        GoldController.TakeGoldForUpgrade(towerUpgradeData.UpgradeCost);

        CloseUIElements();
    }

    private void SellTower()
    {
        towerUpgradeData.TowerToUpgrade.Sell();
        GoldController.AddGoldForTowerSell(towerUpgradeData.SellCost);

        CloseUIElements();
    }

    private void CloseUIElements()
    {
        UIMenusController.CloseUIElement();
    }

    public static void InvokeOnTowerClickEvent(TowerUpgradeData TowerUpgradeData)
    {
        Instance.OnTowerClick?.Invoke(TowerUpgradeData);
        
        //Debug.Log("TowerUpgradeEvents: InvokeOnTowerClickEvent");
    }

    public static void InvokeOnTowerUpgradeEvent()
    {
        Instance.OnTowerUpgrade?.Invoke();
    }

    public static void InvokeOnSellTowerEvent()
    {
        Instance.OnTowerSell?.Invoke();
    }
}
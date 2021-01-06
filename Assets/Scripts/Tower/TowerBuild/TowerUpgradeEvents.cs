using UnityEngine;

public class TowerUpgradeEvents : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UIModalsController UIModalsController;

    public static TowerUpgradeEvents Instance { get { return instance; } }
    private static TowerUpgradeEvents instance;

    public delegate void MethodContainerWithTower(Tower Tower);
    public event MethodContainerWithTower OnTowerClick;

    public delegate void MethodContainer();
    public event MethodContainer OnTowerUpgrade;
    public event MethodContainer OnTowerSell;

    private Tower towerToUpgrade;

    private void Awake()
    {
        instance = this;

        OnTowerClick += OpenUpgradeMenu;

        OnTowerUpgrade += UpgradeTower;
        OnTowerSell += SellTower;
    }

    private void OpenUpgradeMenu(Tower tower)
    {
        towerToUpgrade = tower;
        UIModalsController.OpenUpgradeMenu(towerToUpgrade.transform.position);
        
        //Debug.Log("TowerUpgradeEvents: OpenUpgradeMenu");
    }

    private void UpgradeTower()
    {
        towerToUpgrade.Upgrade();
        
        CloseUIElements();
    }

    private void SellTower()
    {
        towerToUpgrade.Sell();

        CloseUIElements();
    }

    private void CloseUIElements()
    {
        UIModalsController.CloseUIElement();
    }

    public static void InvokeOnTowerClickEvent(Tower tower)
    {
        Instance.OnTowerClick?.Invoke(tower);
        
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
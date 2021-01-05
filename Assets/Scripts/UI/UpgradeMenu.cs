using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button btnUpgrade;
    [SerializeField] private Button btnSell;

    private void Start()
    {
        btnUpgrade.onClick.AddListener(UpgradeTower);
        btnSell.onClick.AddListener(SellTower);
    }

    private void UpgradeTower()
    {
        TowerUpgradeEvents.InvokeOnTowerUpgradeEvent();
    }

    private void SellTower()
    {

    }
}
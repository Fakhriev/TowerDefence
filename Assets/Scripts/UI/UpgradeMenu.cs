using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button btnUpgrade;
    [SerializeField] private Button btnSell;

    private int upgradeCost;

    private void Start()
    {
        btnUpgrade.onClick.AddListener(UpgradeTower);
        btnSell.onClick.AddListener(SellTower);

        GoldController.Instance.OnGoldValueChanged += UpdateUpgradeButtonInteracteble;
    }

    private void UpgradeTower()
    {
        TowerUpgradeEvents.InvokeOnTowerUpgradeEvent();
    }

    private void SellTower()
    {
        TowerUpgradeEvents.InvokeOnSellTowerEvent();
    }

    private void UpdateUpgradeButtonInteracteble(int playerGold)
    {
        btnUpgrade.interactable = playerGold >= upgradeCost;
    }

    public void SetUpgradeData(int upgradeCost, bool isTowerMaxLevel)
    {
        this.upgradeCost = upgradeCost;

        if(isTowerMaxLevel == true)
        {
            btnUpgrade.gameObject.SetActive(false);
        }
        else
        {
            btnUpgrade.gameObject.SetActive(true);
            UpdateUpgradeButtonInteracteble(GoldController.Instance.Gold);
        }
    }
}
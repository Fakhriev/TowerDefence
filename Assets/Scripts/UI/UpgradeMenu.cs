using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button btnUpgrade;
    [SerializeField] private Button btnSell;

    [Header("TMP")]
    [SerializeField] private TextMeshProUGUI tmpUpgradeCost;
    [SerializeField] private TextMeshProUGUI tmpSellCost;

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

    public void SetUpgradeData(int upgradeCost, int sellCost, bool isTowerMaxLevel)
    {
        this.upgradeCost = upgradeCost;

        tmpUpgradeCost.text = upgradeCost.ToString();
        tmpSellCost.text = sellCost.ToString();

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
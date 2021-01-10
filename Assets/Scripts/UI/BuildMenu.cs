using System;
using UnityEngine;
using UnityEngine.UI;

public class BuildMenu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button btnTower1;
    [SerializeField] private Button btnTower2;
    [SerializeField] private Button btnTower3;
    [SerializeField] private Button btnTower4;

    [Header("Towers Data SO")]
    [SerializeField] private TowerData_SO[] towersDataArray;

    private void Start()
    {
        SubscribeAtButtons();

        GoldController.Instance.OnGoldValueChanged += UpdateButtonsInteracteble;
    }

    private void SubscribeAtButtons()
    {
        btnTower1.onClick.AddListener(delegate { TryToBuildTower(TowerTypes.Tower1); });
        btnTower2.onClick.AddListener(delegate { TryToBuildTower(TowerTypes.Tower2); });
        btnTower3.onClick.AddListener(delegate { TryToBuildTower(TowerTypes.Tower3); });
        btnTower4.onClick.AddListener(delegate { TryToBuildTower(TowerTypes.Tower4); });
    }

    private void TryToBuildTower(TowerTypes type)
    {
        if(Array.Find(towersDataArray, TD => TD.TowerData.Type == type) == null)
        {
            Debug.LogWarning("No Tower With This Type In Array");
            return;
        }

        TowerData TowerData = Array.Find(towersDataArray, TD => TD.TowerData.Type == type).TowerData;

        //If Player Has Enough Gold - TODO - Or Make it Automatically With Interactible Button

        TowerBuildEvents.InvokeOnTowerBuildEvent(TowerData);
    }

    private void UpdateButtonsInteracteble(int playerGold)
    {
        btnTower1.interactable = playerGold >= Array.Find(towersDataArray, TD => TD.TowerData.Type == TowerTypes.Tower1).TowerData.BuildCost;
        btnTower2.interactable = playerGold >= Array.Find(towersDataArray, TD => TD.TowerData.Type == TowerTypes.Tower2).TowerData.BuildCost;
        btnTower3.interactable = playerGold >= Array.Find(towersDataArray, TD => TD.TowerData.Type == TowerTypes.Tower3).TowerData.BuildCost;
        btnTower4.interactable = playerGold >= Array.Find(towersDataArray, TD => TD.TowerData.Type == TowerTypes.Tower4).TowerData.BuildCost;
    }

    private void OnEnable()
    {
        UpdateButtonsInteracteble(GoldController.Instance.Gold);
    }
}
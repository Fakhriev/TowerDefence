using UnityEngine;

public class TowerBuildEvents : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UIModalsController UIModalsController;

    [Header("Prefabs")]
    [SerializeField] private GameObject TowerPrefab;

    public static TowerBuildEvents Instance { get { return instance; } }
    private static TowerBuildEvents instance;

    public delegate void MethodContainerWtihFoundation(Foundation Foundation);
    public event MethodContainerWtihFoundation OnTowerFoundationClick;

    public delegate void MethodContainerWtihTowerDataSO(TowerData_SO TowerDataSO);
    public event MethodContainerWtihTowerDataSO OnTowerBuild;

    private Foundation foundation;

    private void Awake()
    {
        instance = this;

        OnTowerFoundationClick += OpenBuildMenu;
        OnTowerBuild += BuildTower;
    }

    private void OpenBuildMenu(Foundation foundation)
    {
        this.foundation = foundation;
        UIModalsController.OpenBuildMenu(foundation.position);
    }

    private void BuildTower(TowerData_SO towerDataSO)
    {
        GameObject createdTower = Instantiate(TowerPrefab, foundation.towerParent);
        createdTower.name = towerDataSO.TowerData.Type.ToString();

        Tower tower = createdTower.GetComponent<Tower>();
        tower.SetupTower(towerDataSO.TowerData, foundation);

        CloseUIElements();
    }

    private void CloseUIElements()
    {
        UIModalsController.CloseUIElement();
    }

    public static void InvokeOnTowerBuildEvent(TowerData_SO towerDataSO)
    {
        Instance.OnTowerBuild(towerDataSO);
    }

    public static void InvokeOnTowerFoundationClickEvent(Foundation foundation)
    {
        Instance.OnTowerFoundationClick(foundation);
    }
}
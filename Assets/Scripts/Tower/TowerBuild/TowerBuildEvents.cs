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

    public delegate void MethodContainerWtihTowerData(TowerData TowerData);
    public event MethodContainerWtihTowerData OnTowerBuild;

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

    private void BuildTower(TowerData towerData)
    {
        GameObject createdTower = Instantiate(TowerPrefab, foundation.towerParent);
        createdTower.name = towerData.Type.ToString();

        Tower tower = createdTower.GetComponent<Tower>();
        tower.SetupTower(towerData, foundation);

        CloseUIElements();
    }

    private void CloseUIElements()
    {
        UIModalsController.CloseUIElement();
    }

    public static void InvokeOnTowerBuildEvent(TowerData towerData)
    {
        Instance.OnTowerBuild(towerData);
    }

    public static void InvokeOnTowerFoundationClickEvent(Foundation foundation)
    {
        Instance.OnTowerFoundationClick(foundation);
    }
}
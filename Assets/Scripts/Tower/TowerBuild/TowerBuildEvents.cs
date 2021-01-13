using UnityEngine;

public class TowerBuildEvents : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UIMenusController UIMenusController;

    [Header("Prefabs")]
    [SerializeField] private GameObject TowerPrefab;

    public static TowerBuildEvents Instance { get { return instance; } }
    private static TowerBuildEvents instance;

    public delegate void MethodContainerWithFoundation(Foundation Foundation);
    public event MethodContainerWithFoundation OnTowerFoundationClick;

    public delegate void MethodContainerWithTowerData(TowerData TowerData);
    public event MethodContainerWithTowerData OnTowerBuild;

    private Foundation foundation;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        OnTowerFoundationClick += OpenBuildMenu;
        OnTowerBuild += BuildTower;
    }

    private void OpenBuildMenu(Foundation foundation)
    {
        this.foundation = foundation;
        UIMenusController.OpenBuildMenu(foundation.position);
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
        UIMenusController.CloseUIElement();
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
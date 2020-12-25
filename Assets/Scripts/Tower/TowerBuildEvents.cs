using UnityEngine;

public class TowerBuildEvents : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TowerBuildingUI TowerBuildingUI;

    public static TowerBuildEvents Instance { get { return instance; } }
    private static TowerBuildEvents instance;

    public delegate void MethodContainer(Foundation Foundation);
    public event MethodContainer OnTowerFoundationClick;

    private Foundation foundation;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        OnTowerFoundationClick += ShowTowersBuildingUI;
    }

    private void ShowTowersBuildingUI(Foundation foundation)
    {
        this.foundation = foundation;

        TowerBuildingUI.StartAnimation(UIAnumationType.Show);
    }

    public static void InvokeOnTowerFoundationClickEvent(Foundation foundation)
    {
        Instance.OnTowerFoundationClick(foundation);
    }
}
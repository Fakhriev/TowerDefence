using UnityEngine;

public class TowerBuildEvents : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UIModalsController UIModalsController;
    [SerializeField] private TowerBuildingUI TowerBuildingUI;

    public static TowerBuildEvents Instance { get { return instance; } }
    private static TowerBuildEvents instance;

    public delegate void MethodContainer(Foundation Foundation);
    public event MethodContainer OnTowerFoundationClick;

    private Foundation foundation;

    private void Awake()
    {
        instance = this;

        OnTowerFoundationClick += OpenBuildMenu;
    }

    private void OpenBuildMenu(Foundation foundation)
    {
        this.foundation = foundation;
        UIModalsController.OpenBuildMenu(foundation.position);
    }

    public static void InvokeOnTowerFoundationClickEvent(Foundation foundation)
    {
        Instance.OnTowerFoundationClick(foundation);
    }
}
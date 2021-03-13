using UnityEngine;

public class UIMenusController : MonoBehaviour
{
    [Header("Prefabs For Create")]
    [SerializeField] private GameObject BuildMenuPrefab;
    [SerializeField] private GameObject UpgradeMenuPrefab;

    [Header("UI Parent")]
    [SerializeField] private Transform menuParent;

    [Header("Main Camera")]
    [SerializeField] private Camera mainCamera;

    private BuildMenu buildMenu;
    private UpgradeMenu upgradeMenu;

    private void Start()
    {
        buildMenu = Instantiate(BuildMenuPrefab, menuParent).GetComponent<BuildMenu>();
        buildMenu.gameObject.SetActive(false);

        upgradeMenu = Instantiate(UpgradeMenuPrefab, menuParent).GetComponent<UpgradeMenu>();
        upgradeMenu.gameObject.SetActive(false);
    }

    private Vector3 GetMenuScreenPosition(Vector3 worldPosition)
    {
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition);
        screenPosition.z = 0;

        return screenPosition;
    }

    public void OpenBuildMenu(Vector3 position)
    {
        if (buildMenu == null)
        {
            Debug.LogWarning("BuildMenu is Null");
            return;
        }

        buildMenu.transform.position = GetMenuScreenPosition(position);
        buildMenu.gameObject.SetActive(true);
    }

    public void OpenUpgradeMenu(Vector3 position, int upgradeCost, int sellCost, bool isTowerMaxLevel)
    {
        if(upgradeMenu == null)
        {
            Debug.LogWarning("UpgradeMenu is Null");
            return;
        }

        upgradeMenu.SetUpgradeData(upgradeCost, sellCost, isTowerMaxLevel);
        upgradeMenu.transform.position = GetMenuScreenPosition(position);
        upgradeMenu.gameObject.SetActive(true);
    }    

    public void CloseUIElement()
    {
        buildMenu?.gameObject.SetActive(false);
        upgradeMenu?.gameObject.SetActive(false);
    }
}
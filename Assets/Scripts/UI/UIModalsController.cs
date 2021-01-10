using UnityEngine;

public class UIModalsController : MonoBehaviour
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

    public void OpenBuildMenu(Vector3 position)
    {
        if (buildMenu == null)
        {
            Debug.LogWarning("BuildMenu is Null");
            return;
        }

        if(buildMenu.transform.position == mainCamera.WorldToScreenPoint(position))
        {
            //Debug.Log("BuildMenu position already on it.");
            //return;
        }

        buildMenu.transform.position = mainCamera.WorldToScreenPoint(position);
        buildMenu.gameObject.SetActive(true);
    }

    public void OpenUpgradeMenu(Vector3 position, int upgradeCost, bool isTowerMaxLevel)
    {
        if(upgradeMenu == null)
        {
            Debug.LogWarning("UpgradeMenu is Null");
            return;
        }

        if (upgradeMenu.transform.position == mainCamera.WorldToScreenPoint(position))
        {
            //Debug.Log("UpgradeMenu position already on it.");
            //return;
        }

        upgradeMenu.SetUpgradeData(upgradeCost, isTowerMaxLevel);
        upgradeMenu.transform.position = mainCamera.WorldToScreenPoint(position);
        upgradeMenu.gameObject.SetActive(true);
    }    

    public void CloseUIElement()
    {
        buildMenu?.gameObject.SetActive(false);
        upgradeMenu?.gameObject.SetActive(false);
    }
}
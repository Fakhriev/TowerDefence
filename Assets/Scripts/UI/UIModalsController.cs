using UnityEngine;

public class UIModalsController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject buildMenu;
    [SerializeField] private GameObject upgradeMenu;

    [Header("Prefabs For Create")]
    [SerializeField] private GameObject BuildMenuPrefab;
    [SerializeField] private GameObject UpgradeMenuPrefab;

    [Header("UI Parent")]
    [SerializeField] private Transform menuParent;

    [Header("Main Camera")]
    [SerializeField] private Camera mainCamera;

    private void Start()
    {
        buildMenu = Instantiate(BuildMenuPrefab, menuParent);
        buildMenu.SetActive(false);

        upgradeMenu = Instantiate(UpgradeMenuPrefab, menuParent);
        upgradeMenu.SetActive(false);
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
        buildMenu.SetActive(true);
    }

    public void OpenUpgradeMenu(Vector3 position)//Also Tower Variation
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

        upgradeMenu.transform.position = mainCamera.WorldToScreenPoint(position);
        upgradeMenu.SetActive(true);
    }    

    public void CloseUIElement()
    {
        buildMenu?.SetActive(false);
        upgradeMenu?.SetActive(false);
    }
}
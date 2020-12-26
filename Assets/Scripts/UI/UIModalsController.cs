using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIModalsController : MonoBehaviour
{
    [SerializeField] private GameObject BuildMenu;
    [SerializeField] private Transform buildMenuParent;
    [SerializeField] private Camera mainCamera;

    public GameObject createdUIElement;

    public void OpenBuildMenu(Vector3 position)
    {
        if (createdUIElement != null)
            Destroy(createdUIElement);

        createdUIElement = Instantiate(BuildMenu, buildMenuParent);
        createdUIElement.transform.position = mainCamera.WorldToScreenPoint(position);
    }

    public void CloseUIElement()
    {
        if (createdUIElement == null)
            return;

        Destroy(createdUIElement);
    }
}
using UnityEngine;
using UnityEngine.EventSystems;

public class TapEventsHandler : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private UIMenusController UIMenusController;

    public void OnPointerClick(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UIMenusController.CloseUIElement();
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }
}
using UnityEngine;
using UnityEngine.EventSystems;

public class TapEventsHandler : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private UIModalsController UIModalsController;

    public void OnPointerClick(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UIModalsController.CloseUIElement();
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }
}
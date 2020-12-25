using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TowerBuildingUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TowerBuildEvents TowerBuildEvents;

    [Header("Tower Building UI Components")]
    [SerializeField] private Animator Animator;
    [SerializeField] private GameObject BackgroundGO;

    [Header("Buttons")]
    [SerializeField] private Button CloseButton;

    private bool isBusy;

    private void Start()
    {
        CloseButton.onClick.AddListener(CloseUI);
        BackgroundGO.SetActive(false);
    }

    private void CloseUI()
    {
        StartAnimation(UIAnumationType.Hide);
    }

    private IEnumerator BecameUnBusyAfterTimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        isBusy = false;
    }

    public void StartAnimation(UIAnumationType animationType)
    {
        if (isBusy == true)
            return;

        Animator.SetTrigger(animationType.ToString());
        StartCoroutine(BecameUnBusyAfterTimer(0.25f));
    }
}

[System.Serializable]
public enum UIAnumationType
{
    Show,
    Hide
}
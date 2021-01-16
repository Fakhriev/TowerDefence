using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameEndModal : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RoundsController RoundsController;

    [Header("Win Things")]
    [SerializeField] private GameObject winModal;
    [SerializeField] private Button btnNext;
    [SerializeField] private Button btnAgainWin;

    [Header("Loose Things")]
    [SerializeField] private GameObject looseModal;
    [SerializeField] private Button btnAgainLoose;

    private IEnumerator ShowLooseModalAfterTimer()
    {
        yield return new WaitForSeconds(1);
        looseModal.SetActive(true);
    }

    public void OpenWinModal()
    {
        winModal.SetActive(true);

        btnNext.onClick.AddListener(RoundsController.NextRound);
        btnAgainWin.onClick.AddListener(RoundsController.AgainThisRound);
    }

    public void OpenLooseModal()
    {
        StartCoroutine(ShowLooseModalAfterTimer());
        btnAgainLoose.onClick.AddListener(RoundsController.AgainThisRound);
    }
}
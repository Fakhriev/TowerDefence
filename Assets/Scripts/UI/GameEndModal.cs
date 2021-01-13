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


    public void OpenWinModal()
    {
        winModal.SetActive(true);

        btnNext.onClick.AddListener(RoundsController.NextRound);
        btnAgainWin.onClick.AddListener(RoundsController.AgainThisRound);
    }

    public void OpenLooseModal()
    {
        looseModal.SetActive(true);

        btnAgainLoose.onClick.AddListener(RoundsController.AgainThisRound);
    }
}
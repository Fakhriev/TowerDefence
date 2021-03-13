using System.Collections;
using TMPro;
using UnityEngine;

public class RoundsController : MonoBehaviour
{
    [Header("Rounds Config")]
    [SerializeField] private GameRoundsConfig_SO gameRoundsConfigSO;

    [Header("Round Number Things")]
    [SerializeField] private Animator RoundNumberAnimator;
    [SerializeField] private TextMeshProUGUI tmpRoundNum;
    [SerializeField] private string roundText;
    [SerializeField] private float roundNumShowTimer;

    [Header("Rounds Save Parametres")]
    [SerializeField] private string playerPrefsIndex;

    private int currentRoundIndex = 0;

    private void RestartScene()
    {
        SceneLoader.RestartThisScene();
    }

    private IEnumerator HideRoundNumber()
    {
        yield return new WaitForSeconds(roundNumShowTimer);
        RoundNumberAnimator.SetTrigger("RoundNumberDisappear");
    }

    public int GetCurrentRoundIndex()
    {
        int roundIndex = 0;

        if(PlayerPrefs.HasKey(playerPrefsIndex) == true)
        {
            roundIndex = PlayerPrefs.GetInt(playerPrefsIndex);
        }

        currentRoundIndex = roundIndex;
        return roundIndex;
    }

    public Round GetCurrentRound()
    {
        Round value = new Round();
        int lastCreatedRoundIndex = gameRoundsConfigSO.Rounds.Length - 1;

        if (currentRoundIndex > lastCreatedRoundIndex)
        {
            value = gameRoundsConfigSO.Rounds[lastCreatedRoundIndex];
        }
        else
        {
            value = gameRoundsConfigSO.Rounds[currentRoundIndex];
        }

        return value;
    }

    public void ShowCurrentRoundInUI()
    {
        tmpRoundNum.text = $"{roundText} {currentRoundIndex + 1}";
        RoundNumberAnimator.SetTrigger("RoundNumberAppear");

        StartCoroutine(HideRoundNumber());
    }

    public void NextRound()
    {
        if(PlayerPrefs.HasKey(playerPrefsIndex) == true)
        {
            int nextRound = PlayerPrefs.GetInt(playerPrefsIndex);
            nextRound++;
            PlayerPrefs.SetInt(playerPrefsIndex, nextRound);
        }
        else
        {
            PlayerPrefs.SetInt(playerPrefsIndex, 1);
        }

        RestartScene();
    }

    public void AgainThisRound()
    {
        RestartScene();
    }
}
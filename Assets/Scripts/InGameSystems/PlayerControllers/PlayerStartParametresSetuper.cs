using UnityEngine;

public class PlayerStartParametresSetuper : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GoldController GoldController;
    [SerializeField] private PlayerHealthController PlayerHealthController;

    [Header("Player Parametres SO")]
    [SerializeField] private PlayerParametres_SO playerParametres;

    private void Awake()
    {
        SetupPlayerStartStats();
    }

    private void SetupPlayerStartStats()
    {
        PlayerHealthController.SetupStartHealth(playerParametres.Health);
        GoldController.SetStartGoldValue(playerParametres.Gold);
    }
}
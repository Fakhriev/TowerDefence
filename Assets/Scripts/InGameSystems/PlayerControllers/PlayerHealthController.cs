using TMPro;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [Header("Health UI Things")]
    [SerializeField] private TextMeshProUGUI tmpHealth;
    [SerializeField] private string healthText;

    private int startPlayerHealth;
    private int playerHealth;

    private bool isPlayerDead;

    private void PlayerDie()
    {
        isPlayerDead = true;
        GameEndEvents.InvokeOnGameEndEvent(GameEndType.Loose);
    }

    private void UpdateHealthText()
    {
        tmpHealth.text = $"{healthText} {playerHealth}";
    }

    public void DamagePlayer(int damage)
    {
        if (isPlayerDead == true)
            return;

        playerHealth -= damage;

        if(playerHealth <= 0)
        {
            playerHealth = 0;
            PlayerDie();
        }

        UpdateHealthText();
    }

    public void SetupStartHealth(int health)
    {
        startPlayerHealth = health;
        playerHealth = startPlayerHealth;

        UpdateHealthText();
    }
}
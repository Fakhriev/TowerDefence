using UnityEngine;

public class EnemyDamageEvents : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerHealthController PlayerHealthController;

    public static EnemyDamageEvents Instance { get { return instance; } }
    private static EnemyDamageEvents instance;

    public delegate void MethodContainerWithInt(int damage);
    public event MethodContainerWithInt OnDamagePlayerByEnemy;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        OnDamagePlayerByEnemy += DamagePlayer;
    }

    private void DamagePlayer(int damage)
    {
        PlayerHealthController.DamagePlayer(damage);
    }

    public static void InvokeOnDamagePlayerByEnemyEvent(int damage)
    {
        Instance.OnDamagePlayerByEnemy?.Invoke(damage);
    }
}
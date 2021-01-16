using UnityEngine;

public class EnemyKillEvents : MonoBehaviour
{
    public static EnemyKillEvents Instance { get { return instance; } }
    private static EnemyKillEvents instance;

    public delegate void MethodContainerWithEnemyData(EnemyData EnemyData);
    public event MethodContainerWithEnemyData OnEnemyDie;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        OnEnemyDie += EnemyDie;
    }

    private void EnemyDie(EnemyData enemyData)
    {

    }

    public static void InvokeOnEnemyDieEvent(EnemyData enemyData)
    {
        Instance.OnEnemyDie?.Invoke(enemyData);
    }
}
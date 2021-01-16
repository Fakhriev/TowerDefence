using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Enemy Enemy;

    private int damageByEnemy;

    private void Start()
    {
        Enemy.OnDie += EnemyDie;
    }

    private void Attack()
    {
        EnemyDamageEvents.InvokeOnDamagePlayerByEnemyEvent(damageByEnemy);
    }

    private void StopAttack()
    {
        CancelInvoke();
    }

    private void EnemyDie()
    {
        Enemy.OnDie -= EnemyDie;
        StopAttack();
    }

    public void StartAttack()
    {
        InvokeRepeating("Attack", 0, 1);
    }

    public void SetEnemyDamage(int damagePerSecond)
    {
        damageByEnemy = damagePerSecond;
    }
}
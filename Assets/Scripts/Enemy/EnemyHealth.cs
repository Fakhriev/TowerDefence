using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Enemy Enemy;

    private int health;

    public void SetEnemyhealth(int Health)
    {
        health = Health;
    }

    public void DamageToHealth(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            health = 0;
            Enemy.Die();
        }
    }
}
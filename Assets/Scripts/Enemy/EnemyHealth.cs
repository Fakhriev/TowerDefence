using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Enemy Enemy;

    [Header("Enemy Health Parametres")]
    [SerializeField] private int health;
    
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
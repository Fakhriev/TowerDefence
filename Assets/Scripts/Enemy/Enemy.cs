using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemyMovement EnemyMovement;
    [SerializeField] private EnemyHealth EnemyHealth;

    [Header("Enemy Components")]
    [SerializeField] private BoxCollider boxCollider;

    [Header("Mesh Components")]
    [SerializeField] private Transform meshPrefabParent;
    [SerializeField] private DefaultEnemyMeshComponenets defaultComponents;

    [Header("Enemy Parametres")]
    [SerializeField] private float deactivateTimer;

    private EnemyData myEnemyData;

    private bool isDead;

    public delegate void MethodContainer();
    public event MethodContainer OnDie;

    private void SetupEnemyMeshPrefab()
    {
        GameObject createdMeshPrefab = Instantiate(myEnemyData.MeshPrefab, meshPrefabParent);
        EnemyMeshPrefab enemyMeshPrefab = createdMeshPrefab.GetComponent<EnemyMeshPrefab>();
        EnemyMovement.SetupAnimator(enemyMeshPrefab.Animator);
    }

    private void SetupEnemyStats()
    {
        EnemyMovement.SetupSpeed(myEnemyData.EnemyStats.Speed);
        EnemyHealth.SetEnemyhealth(myEnemyData.EnemyStats.Health);
        //EnemyAttack.SetEnemyDamage(myEnemyData.EnemyStats.DamagePerSecond); TODO
    }

    private IEnumerator MakeEnemyNonActives()
    {
        yield return new WaitForSeconds(deactivateTimer);
        gameObject.SetActive(false);
    }

    public void Hit(int damage)
    {
        EnemyHealth.DamageToHealth(damage);
    }

    public void Die()
    {
        if (isDead == true)
            return;

        boxCollider.enabled = false;

        isDead = true;
        OnDie?.Invoke();

        StartCoroutine(MakeEnemyNonActives());
    }

    public void Spawn(MovePoint[] movePoints, EnemyData enemyData)
    {
        defaultComponents.DeactivateDefaultComponenets();
        myEnemyData = enemyData;

        SetupEnemyMeshPrefab();
        SetupEnemyStats();

        EnemyMovement.SetupMovePoints(movePoints);
    }
}

[Serializable]
public class DefaultEnemyMeshComponenets
{
    [SerializeField] private Animator defaultAnimator;
    [SerializeField] private GameObject defaultMeshPrefab;
    [SerializeField] private GameObject defaultHips;

    public void DeactivateDefaultComponenets()
    {
        defaultAnimator.enabled = false;
        defaultMeshPrefab.SetActive(false);
        defaultHips.SetActive(false);
    }
}
using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemyHealth EnemyHealth;

    [Header("Enemy Components")]
    [SerializeField] private BoxCollider boxCollider;

    [Header("Mesh Components")]
    [SerializeField] private Transform meshPrefabParent;
    [SerializeField] private DefaultEnemyMeshComponenets defaultComponents;

    [Header("Enemy Parametres")]
    [SerializeField] private float deactivateTimer;

    public delegate void MethodContainer();
    public event MethodContainer OnDie;

    private MovePoint[] movePointsArray;
    private MovePoint nextMovePoint;

    private bool isMoving;
    private bool isDead;

    /// <summary>
    /// Fields From EnemyData
    /// </summary>
    public EnemyData myEnemyData;
    private Animator animator;
    private float speed;

    private void Update()
    {
        if (isMoving == false)
            return;

        Vector3 newPosition = Vector3.MoveTowards(transform.position, nextMovePoint.position, speed * Time.deltaTime);
        transform.position = newPosition;

        if (transform.position == nextMovePoint.position)
            SetNewNextMovePoint();
    }

    private void SetNewNextMovePoint()
    {
        if(movePointsArray[nextMovePoint.index].type == MovePointType.FinishPoint)
        {
            StopEnemy(EnemyAnimationTrigger.Attack);
            return;
        }

        transform.LookAt(movePointsArray[nextMovePoint.index + 1].transform);
        nextMovePoint = movePointsArray[nextMovePoint.index + 1];
    }

    private void StopEnemy(EnemyAnimationTrigger animTrigger)
    {
        isMoving = false;
        animator.SetTrigger(animTrigger.ToString());
    }

    private void SetupEnemyMeshPrefab()
    {
        GameObject createdMeshPrefab = Instantiate(myEnemyData.MeshPrefab, meshPrefabParent);
        EnemyMeshPrefab enemyMeshPrefab = createdMeshPrefab.GetComponent<EnemyMeshPrefab>();
        animator = enemyMeshPrefab.Animator;
    }

    private void SetupEnemyStats()
    {
        EnemyHealth.SetEnemyhealth(myEnemyData.EnemyStats.Health);
        //EnemyAttack.SetEnemyDamage(myEnemyData.EnemyStats.DamagePerSecond); TODO
        speed = myEnemyData.EnemyStats.Speed;
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
        StopEnemy(EnemyAnimationTrigger.Die);

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

        movePointsArray = movePoints;

        transform.position = Array.Find(movePointsArray, point => point.type == MovePointType.StartPoint).position;
        nextMovePoint = Array.Find(movePointsArray, point => point.type == MovePointType.Point);

        transform.LookAt(nextMovePoint.transform);
        isMoving = true;
    }
}

public enum EnemyAnimationTrigger
{
    Attack,
    Die
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
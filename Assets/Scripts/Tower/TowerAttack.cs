using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject defaultArrowPrefab;

    [Header("Tower Attack Components")]
    [SerializeField] private Transform arrowsPull;

    [Header("Tower Attack Parametres")]
    [SerializeField] private int arrowsInPull;

    private float attackSpeed;
    private Transform shootingPosition;

    public Enemy currentEnemyTarget;// To Private
    public bool needToAttack;// To Delete

    private Arrow[] arrowPull = new Arrow[0];

    private void Start()
    {
        CreateArrowsPull();
    }

    private void Update()
    {
        if(needToAttack == true)
        {
            SetTarget(currentEnemyTarget);
            needToAttack = false;
        }
    }

    private void CreateArrowsPull()
    {
        List<Arrow> arrowList = new List<Arrow>();

        for(int i = 0; i < arrowsInPull; i++)
        {
            GameObject createdArrow = Instantiate(defaultArrowPrefab, arrowsPull);
            
            Arrow arrow = createdArrow.GetComponent<Arrow>();
            arrow.SetArrow(this);

            arrowList.Add(arrow);
        }

        arrowPull = arrowList.ToArray();
    }

    private void Shoot()
    {
        if(currentEnemyTarget == null)
        {
            Debug.LogWarning("TowerAttack: Current Enemy Target Is Null");
            return;
        }

        if(Array.Find(arrowPull, arrowInPull => arrowInPull.IsReady() == true) != null)
        {
            Arrow arrow = Array.Find(arrowPull, arrowInPull => arrowInPull.IsReady() == true);
            arrow.transform.position = shootingPosition.position;
            arrow.SetTarget(currentEnemyTarget);
        }
        else
        {
            arrowPull[0].ForceHit();
            Shoot();
        }
    }


    public void SetAttackSpeed(float attacksInSecond)
    {
        attackSpeed = 1 / attacksInSecond;
        //Debug.Log($"My attack speed is {attackSpeed}. Attacks in second: {attacksInSecond}");
    }

    public void SetShootingPosition(Transform shootingPosition)
    {
        this.shootingPosition = shootingPosition;
    }

    public void SetTarget(Enemy target)
    {
        currentEnemyTarget = target;

        if(attackSpeed == 0)
        {
            Debug.LogWarning($"TowerAttack: [{gameObject.name}] can't start shooting because attackSpeed is 0");
            return;
        }

        InvokeRepeating("Shoot", 0, attackSpeed);
    }

    public void StopShooting()
    {
        currentEnemyTarget = null;

        CancelInvoke();
    }
}
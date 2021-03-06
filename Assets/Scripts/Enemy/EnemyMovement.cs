﻿using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Enemy Enemy;
    [SerializeField] private EnemyAttack EnemyAttack;

    private Animator animator;
    private float speed;

    private MovePoint[] movePointsArray;
    private MovePoint nextMovePoint;

    private bool isMoving;

    private void Start()
    {
        Enemy.OnDie += EnemyDie;
        GameEndEvents.Instance.OnGameEnd += EnemyWin;
    }

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
        if (movePointsArray[nextMovePoint.index].type == MovePointType.FinishPoint)
        {
            EnemyStartAttack();
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

    private void EnemyStartAttack()
    {
        StopEnemy(EnemyAnimationTrigger.Attack);
        EnemyAttack.StartAttack();
    }

    private void EnemyDie()
    {
        Enemy.OnDie -= EnemyDie;
        GameEndEvents.Instance.OnGameEnd -= EnemyWin;

        StopEnemy(EnemyAnimationTrigger.Die);
    }

    private void EnemyWin(GameEndType gameEndType)
    {
        StopEnemy(EnemyAnimationTrigger.Victory);
    }

    public void SetupMovePoints(MovePoint[] movePoints)
    {
        movePointsArray = movePoints;

        transform.position = Array.Find(movePointsArray, point => point.type == MovePointType.StartPoint).position;
        nextMovePoint = Array.Find(movePointsArray, point => point.type == MovePointType.Point);

        transform.LookAt(nextMovePoint.transform);
    }

    public void StartMoving()
    {
        isMoving = true;
    }

    public void SetupAnimator(Animator animator)
    {
        this.animator = animator;
    }

    public void SetupSpeed(float speed)
    {
        this.speed = speed;
    }
}

[Serializable]
public enum EnemyAnimationTrigger
{
    Attack,
    Die,
    Victory
}
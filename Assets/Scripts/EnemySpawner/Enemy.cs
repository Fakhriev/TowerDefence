using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Components")]
    [SerializeField] private Animator Animator;

    [Header("Enemy Parametres")]
    [SerializeField] private float speed;

    private MovePoint[] movePointsArray;
    private MovePoint nextMovePoint;

    private bool isMoving;

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
            StopEnemy();
            return;
        }

        transform.LookAt(movePointsArray[nextMovePoint.index + 1].transform);
        nextMovePoint = movePointsArray[nextMovePoint.index + 1];
    }

    private void StopEnemy()
    {
        isMoving = false;
        Animator.SetTrigger("Attack");
    }

    public void Spawn(MovePoint[] movePoints)
    {
        movePointsArray = movePoints;

        transform.position = Array.Find(movePointsArray, point => point.type == MovePointType.StartPoint).position;
        nextMovePoint = Array.Find(movePointsArray, point => point.type == MovePointType.Point);

        transform.LookAt(nextMovePoint.transform);
        isMoving = true;
    }
}
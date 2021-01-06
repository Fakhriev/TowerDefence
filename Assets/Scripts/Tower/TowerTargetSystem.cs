using System.Collections.Generic;
using UnityEngine;

public class TowerTargetSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TowerAttack TowerAttack;

    [Header("Target System Components")]
    [SerializeField] private SphereCollider sphereCollider;

    public List<GameObject> enemiesInRange = new List<GameObject>();
    public Enemy currentEnemyTarget;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Enemy")
        {
            AddNewEnemyToList(collider.gameObject);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Enemy")
        {
            RemoveEnemyFromList(collider.gameObject);
        }
    }

    private void AddNewEnemyToList(GameObject enemyGO)
    {
        enemiesInRange.Add(enemyGO);

        if (currentEnemyTarget == null)
        {
            currentEnemyTarget = enemyGO.GetComponent<Enemy>();
            SetTargetToAttackSystem();
        }
    }

    private void RemoveEnemyFromList(GameObject enemyGO)
    {
        enemiesInRange.Remove(enemyGO);

        if (currentEnemyTarget.gameObject == enemyGO)
        {
            currentEnemyTarget = null;
            TowerAttack.StopShooting();

            if (enemiesInRange.Count > 0)
                SetClosestEnemyAsTarget();
        }
    }

    private void SetClosestEnemyAsTarget()
    {
        GameObject enemyGO = enemiesInRange[0];

        foreach(GameObject enemyInList in enemiesInRange)
        {
            if(Vector3.Distance(transform.position, enemyInList.transform.position) < Vector3.Distance(transform.position, enemyGO.transform.position))
            {
                enemyGO = enemyInList;
            }
        }

        currentEnemyTarget = enemyGO.GetComponent<Enemy>();
        SetTargetToAttackSystem();
    }

    private void SetTargetToAttackSystem()
    {
        TowerAttack.SetTarget(currentEnemyTarget);
    }

    public void SetupRange(int range)
    {
        sphereCollider.radius = range;
    }
}
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
            SetTargetToAttackSystem(enemyGO);
        }
    }

    private void RemoveEnemyFromList(GameObject enemyGO)
    {
        enemiesInRange.Remove(enemyGO);

        if (currentEnemyTarget.gameObject == enemyGO)
        {
            currentEnemyTarget.OnDie -= CurrentTargetIsDie;
            currentEnemyTarget = null;

            TowerAttack.StopShooting();
            SetClosestEnemyAsTarget();
        }
    }

    private void SetClosestEnemyAsTarget()
    {
        if (enemiesInRange.Count == 0)
        {
            //Debug.Log("Tower Target System: No Enemy In Range");
            return;
        }

        GameObject enemyGO = enemiesInRange[0];

        foreach(GameObject enemyInList in enemiesInRange)
        {
            if(Vector3.Distance(transform.position, enemyInList.transform.position) < Vector3.Distance(transform.position, enemyGO.transform.position))
            {
                enemyGO = enemyInList;
            }
        }

        SetTargetToAttackSystem(enemyGO);
    }

    private void CurrentTargetIsDie()
    {
        RemoveEnemyFromList(currentEnemyTarget.gameObject);
    }

    private void SetTargetToAttackSystem(GameObject enemyGO)
    {
        currentEnemyTarget = enemyGO.GetComponent<Enemy>();
        currentEnemyTarget.OnDie += CurrentTargetIsDie;

        TowerAttack.SetTarget(currentEnemyTarget);
    }

    private void OnDestroy()
    {
        if(currentEnemyTarget != null)
        {
            currentEnemyTarget.OnDie -= CurrentTargetIsDie;
        }
    }

    public void SetupRange(int range)
    {
        sphereCollider.radius = range;
    }
}
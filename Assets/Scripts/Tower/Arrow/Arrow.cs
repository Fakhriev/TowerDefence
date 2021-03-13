using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("Arrow Componnents")]
    [SerializeField] private GameObject meshParent;
    [SerializeField] private TrailRenderer trailRenderer;

    [Header("Arrow Parametres")]
    [SerializeField] private float forwardFlySpeed;
    [SerializeField] private float rangeToHitTheTarget;

    private Enemy enemyTarget;

    private int damage;
    private bool isMovingToTarget;

    private TowerAttack myTowerAttack;

    private void Update()
    {
        if (isMovingToTarget == false || enemyTarget == null)
            return;

        transform.LookAt(enemyTarget.transform);
        transform.position = transform.position + transform.forward * forwardFlySpeed * Time.deltaTime;

        if(Vector3.Distance(transform.position, enemyTarget.transform.position) <= rangeToHitTheTarget)
        {
            HitTheTarget();
        }
    }

    private void HitTheTarget()
    {
        enemyTarget.Hit(damage);
        enemyTarget = null;

        isMovingToTarget = false;
        meshParent.SetActive(false);

        trailRenderer.enabled = false;
    }

    public void ForceHit()
    {
        HitTheTarget();
    }

    public void SetTarget(Enemy enemyTarget, int damage)
    {
        this.enemyTarget = enemyTarget;
        this.damage = damage;

        isMovingToTarget = true;
        meshParent.SetActive(true);

        trailRenderer.enabled = true;
        trailRenderer.Clear();
    }

    public void SetArrow(TowerAttack towerAttack, int damage)
    {
        myTowerAttack = towerAttack;
        this.damage = damage;

        enemyTarget = null;

        meshParent.SetActive(false);
        isMovingToTarget = false;
    }

    public bool IsReady()
    {
        if (isMovingToTarget == false && enemyTarget == null)
            return true;
        else
            return false;
    }
}
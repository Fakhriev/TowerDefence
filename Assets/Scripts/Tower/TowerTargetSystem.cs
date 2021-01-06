using UnityEngine;

public class TowerTargetSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TowerAttack TowerAttack;

    [Header("Target System Components")]
    [SerializeField] private SphereCollider sphereCollider;

    private void SetTargetToAttackSystem()
    {
        Enemy enemyTarget = new Enemy();//TODO
        TowerAttack.SetTarget(enemyTarget);
    }

    public void SetupRange(int range)
    {
        sphereCollider.radius = range;
    }
}
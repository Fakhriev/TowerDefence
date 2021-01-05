using UnityEngine;

public class TowerTargetSystem : MonoBehaviour
{
    [Header("Target System Components")]
    [SerializeField] private SphereCollider sphereCollider;

    public void SetupRange(int range)
    {
        sphereCollider.radius = range / 2;
    }
}
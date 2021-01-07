using UnityEngine;

public class EnemyMeshPrefab : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public Animator Animator
    {
        get
        {
            return animator;
        }
    }
}
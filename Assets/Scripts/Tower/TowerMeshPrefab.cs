using UnityEngine;

public class TowerMeshPrefab : MonoBehaviour
{
    [SerializeField] private Transform shootinPosition;

    public Transform ShootingPosition
    {
        get
        {
            return shootinPosition;
        }
    }
}
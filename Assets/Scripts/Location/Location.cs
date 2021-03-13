using UnityEngine;

public class Location : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MovePoints movePoints;

    public MovePoints MovePoints
    {
        get
        {
            return movePoints;
        }
    }
}
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyData", menuName = "Enemy Data SO", order = 51)]
public class EnemyData_SO : ScriptableObject
{
    [SerializeField] private EnemyData enemyData;

    public EnemyData EnemyData
    {
        get
        {
            return enemyData;
        }
    }
}
using UnityEngine;

[CreateAssetMenu(fileName = "New TowerData", menuName = "Tower Data SO", order = 51)]
public class TowerData_SO : ScriptableObject
{
    [SerializeField] private TowerData towerData;

    public TowerData TowerData
    {
        get
        {
            return towerData;
        }
    }
}
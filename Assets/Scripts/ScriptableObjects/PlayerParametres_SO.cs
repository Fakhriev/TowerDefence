using UnityEngine;

[CreateAssetMenu(fileName = "Player Parametres", menuName = "Player Parametres SO", order = 51)]
public class PlayerParametres_SO : ScriptableObject
{
    [Header("Start Parametres")]
    [SerializeField] private int health;
    [SerializeField] private int gold;

    public int Health
    {
        get
        {
            return health;
        }
    }

    public int Gold
    {
        get
        {
            return gold;
        }
    }
}
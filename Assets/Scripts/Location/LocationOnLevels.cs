using UnityEngine;

[System.Serializable]
public struct LocationOnLevels
{
    [Header("From to Levels")]
    [SerializeField] private int fromLevel;
    [SerializeField] private int toLevel;

    [Header("Location Prefab")]
    [SerializeField] private GameObject locationPrefab;

    public bool IsLevelInThisLocationRange(int level)
    {
        if (level >= fromLevel && level <= toLevel)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public GameObject LocationPrefab
    {
        get
        {
            return locationPrefab;
        }
    }
}
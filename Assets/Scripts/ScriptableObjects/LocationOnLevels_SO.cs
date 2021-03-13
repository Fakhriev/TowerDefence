using UnityEngine;

[CreateAssetMenu(fileName = "Location on Levels Config", menuName = "Location on Levels SO", order = 51)]
public class LocationOnLevels_SO : ScriptableObject
{
    [Header("Location On Levels")]
    [SerializeField] private LocationOnLevels[] locationOnLevels = new LocationOnLevels[0];

    public LocationOnLevels[] LocationOnLevels
    {
        get
        {
            return locationOnLevels;
        }
    }
}
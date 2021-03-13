using System;
using UnityEngine;

public class LocationSetuper : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RoundsController RoundsController;
    [SerializeField] private EnemySpawner EnemySpawner;

    [Header("References to Scriptable Objects")]
    [SerializeField] private LocationOnLevels_SO LocationOnLevels_SO;

    [Header("Default Parametres")]
    [SerializeField] private GameObject defaultLocation;


    private void Start()
    {
        Destroy(defaultLocation);
        SpawnLocationOfCurrentRound();
    }

    private void SpawnLocationOfCurrentRound()
    {
        int currentRoundIndex = RoundsController.GetCurrentRoundIndex();
        LocationOnLevels[] locationOnLevelsArray = LocationOnLevels_SO.LocationOnLevels;
        LocationOnLevels currentLevelLocation = FindLocationOfLevel(currentRoundIndex, locationOnLevelsArray);

        GameObject spawnedLocation = Instantiate(currentLevelLocation.LocationPrefab, transform);
        Location location = spawnedLocation.GetComponent<Location>();

        EnemySpawner.PrepareToSpwanEnemyPull(location.MovePoints);
    }

    private LocationOnLevels FindLocationOfLevel(int level, LocationOnLevels[] locationOnLevelsArray)
    {
        if ( Array.Exists(locationOnLevelsArray, locOnLevels => locOnLevels.IsLevelInThisLocationRange(level) == true) )
        {
            Debug.Log($"Location on level [{level}] Exist.");
            return Array.Find(locationOnLevelsArray, locOnLevels => locOnLevels.IsLevelInThisLocationRange(level) == true);
        }
        else
        {
            Debug.Log($"Location on level [{level}] NOT Exist.");
            return GetLastLocationInArray(locationOnLevelsArray);
        }
    }

    private LocationOnLevels GetLastLocationInArray(LocationOnLevels[] locationOnLevelsArray)
    {
        int lastIndexOfArray = locationOnLevelsArray.Length - 1;
        return locationOnLevelsArray[lastIndexOfArray];
    }
}

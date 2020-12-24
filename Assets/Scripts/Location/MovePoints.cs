using System.Collections.Generic;
using UnityEngine;

public class MovePoints : MonoBehaviour
{
    [Header("MovePoints Parametres")]
    [SerializeField] private bool isNeedToSetupPointsArrayOnAwake = true;

    [Header("Points Array")]
    [SerializeField] private MovePoint[] pointsArray = new MovePoint[0];

    private bool isCubesActivated = true;

    private void Awake()
    {
        if (isNeedToSetupPointsArrayOnAwake)
            SetupPointsArray();
    }

    public void SetupPointsArray()
    {
        List<MovePoint> pointsList = new List<MovePoint>();
        int index = 0;

        foreach(Transform child in transform)
        {
            MovePoint point = new MovePoint();

            point.index = index;
            point.type = MovePointType.Point;

            point.transform = child;
            point.position = child.position;

            if (child.name == "Start")
                point.type = MovePointType.StartPoint;

            if (child.name == "Finish")
                point.type = MovePointType.FinishPoint;

            pointsList.Add(point);
            index++;
        }

        pointsArray = pointsList.ToArray();
    }

    public void ChangeCubesState()
    {
        if(pointsArray.Length == 0)
        {
            Debug.LogWarning("Can't Change Cubes State because Points Array is Empty");
            return;
        }

        foreach (MovePoint point in pointsArray)
        {
            point.transform.GetChild(0).gameObject.SetActive(!isCubesActivated);
        }

        isCubesActivated = !isCubesActivated;
    }

    public bool IsCubesActivated
    {
        get
        {
            return isCubesActivated;
        }
    }

    public MovePoint[] PointsArray
    {
        get
        {
            return pointsArray;
        }
    }
}

[System.Serializable]
public class MovePoint
{
    public int index;
    public MovePointType type;

    public Transform transform;
    public Vector3 position;
}

[System.Serializable]
public enum MovePointType
{
    Point,
    StartPoint,
    FinishPoint
}
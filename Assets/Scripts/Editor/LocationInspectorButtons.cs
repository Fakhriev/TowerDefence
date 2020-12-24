using UnityEngine;
using UnityEditor;

public class LocationInspectorButtons : Editor
{

}

[CustomEditor(typeof(MovePoints))]
public class MovePointsButtons : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MovePoints MovePoints = (MovePoints)target;
        EditorGUI.BeginChangeCheck();

        if (GUILayout.Button("Setup Points Array"))
        {
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "MovePoints Setup Points Array");
                MovePoints.SetupPointsArray();
            }
        }

        string changeCubesStateButtonText = MovePoints.IsCubesActivated ? "Deactivate Cubes" : "Activate Cubes";

        if (GUILayout.Button(changeCubesStateButtonText))
        {
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "MovePoints Change Cubes State");
                MovePoints.ChangeCubesState();
            }
        }
    }
}
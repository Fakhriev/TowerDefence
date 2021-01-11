using UnityEngine;
using UnityEditor;

public class ConfigsButtons : Editor
{

}

[CustomEditor(typeof(GameRoundsConfig_SO))]
public class GameRoundsConfigSOButtons: Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameRoundsConfig_SO GameRoundsConfigSO = (GameRoundsConfig_SO)target;
        EditorGUI.BeginChangeCheck();

        if(GUILayout.Button("Add New Round"))
        {
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Add New Round In GameRoundsConfig_SO");
                GameRoundsConfigSO.AddNewRound();
            }
        }
    }
}
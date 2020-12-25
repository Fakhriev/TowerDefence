using UnityEditor;
using UnityEngine;

public class TowerInspectorButtons : Editor
{

}

[CustomEditor(typeof(TowerFoundation))]
public class TowerFoundationButtons : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TowerFoundation TowerFoundation = (TowerFoundation)target;
        EditorGUI.BeginChangeCheck();

        if(GUILayout.Button("Setup Foundation Components"))
        {
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "TowerFoundation Setup Foundation Components");
                TowerFoundation.SetupFoundationComponents();
            }
        }
    }
}
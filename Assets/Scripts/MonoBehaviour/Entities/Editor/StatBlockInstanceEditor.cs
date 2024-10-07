using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StatBlockInstance))]
public class StatBlockInstanceEditor : Editor
{
    SerializedProperty statsArray;

    void OnEnable()
    {
        statsArray = serializedObject.FindProperty("Stats");
        
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        base.OnInspectorGUI();
        serializedObject.ApplyModifiedProperties();
    }
}

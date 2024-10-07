using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameEvent))]
internal class GameEventEditor : Editor
{
    GameEvent instance;

    private void OnEnable()
    {
        instance = (GameEvent) target;
    }
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Raise"))
            instance.Raise();
    }
}
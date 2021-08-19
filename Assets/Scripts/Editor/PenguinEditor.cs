using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Penguin))]
public class PenguinEditor : Editor
{
    private Penguin _penguin;
    private void Awake()
    {
        if (_penguin == null)
            _penguin = target as Penguin;
    }

    override public void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.RepeatButton("Add Force"))
        {
            _penguin.AddForce();
        }
    }
}
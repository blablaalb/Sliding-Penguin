using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(PenguinFSM))]
public class PenguinFSMEditor : Editor
{
    private PenguinFSM _fsmTarget;
    GUIStyle _style = new GUIStyle();

    public override void OnInspectorGUI()
    {
        _style.normal.textColor = Color.white;
        _fsmTarget = target as PenguinFSM;
        DrawDefaultInspector();

        if (EditorApplication.isPlaying || EditorApplication.isPaused)
        {
            
            EditorGUILayout.LabelField("Current State: ", _fsmTarget.GetCurrentState().ToString(), _style);
        }
    }
}

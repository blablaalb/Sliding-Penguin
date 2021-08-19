using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(PlatformChunkVariationManager))]
public class PlatformChunkVariationManagerEditor : Editor
{
    private PlatformChunkVariationManager _targetManager;

    public override void OnInspectorGUI()
    {
        _targetManager = target as PlatformChunkVariationManager;
        DrawDefaultInspector();

        if (GUILayout.Button("Random Variation"))
        {
            _targetManager.SpawnRandomVariation();
        }
    }
}
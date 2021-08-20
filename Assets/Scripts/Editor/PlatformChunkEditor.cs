using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(PlatformChunk))]
public class PlatformChunkEditor : Editor
{
    private PlatformChunk _platformChunk;

    public override void OnInspectorGUI()
    {
        _platformChunk = target as PlatformChunk;
        DrawDefaultInspector();

        if (GUILayout.Button("Random Variation"))
        {
            _platformChunk.SpawnRandomVariation();
        }
    }
}
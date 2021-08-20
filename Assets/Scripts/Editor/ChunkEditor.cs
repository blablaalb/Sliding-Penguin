using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;

/// <summary>
/// This editor places the actve chunk of platforms at the end of the last chunk.
/// This scripts assumes that the chunks are places only along the Z axis. This script will not work otherwise.
/// </summary>
[CustomEditor(typeof(PlatformChunk))]
public class ChunkEditor : Editor
{
    private int _generateAmount;
    private PlatformChunk _targetChunk;

    public override void OnInspectorGUI()
    {
        _targetChunk = target as PlatformChunk;

        DrawDefaultInspector();
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        if (GUILayout.Button("Place Last"))
        {
            PlatformChunk lastPlatofrmChunk = GetLastPlatformChunk();
            if (lastPlatofrmChunk != _targetChunk)
            {
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(_targetChunk.gameObject.transform, "Moving Chunk at The Last Position");
                    Vector3 position = GetChunkEndInZAxis(lastPlatofrmChunk);
                    PlaceChunkAt(position);
                }
            }
        }

        EditorGUILayout.BeginHorizontal();
        EditorGUI.BeginChangeCheck();
        if (GUILayout.Button("Generate"))
        {
            if (EditorGUI.EndChangeCheck())
            {
                for (int i = 0; i < _generateAmount; i++)
                {
                    GameObject newChunkGO = Instantiate<GameObject>(_targetChunk.gameObject);
                    newChunkGO = newChunkGO.GetComponent<PlatformChunk>().SpawnRandomVariation().gameObject;
                    PlatformChunk lastPlatformChunk = GetLastPlatformChunk();
                    newChunkGO.transform.position = GetChunkEndInZAxis(lastPlatformChunk);
                    newChunkGO.transform.SetParent(_targetChunk.transform.parent);
                    Undo.RegisterCreatedObjectUndo(newChunkGO, "Crated a new chunk.");
                }
            }
        }
        _generateAmount = EditorGUILayout.IntField("Amount", _generateAmount);
        EditorGUILayout.EndHorizontal();
    }

    private static Vector3 GetChunkEndInZAxis(PlatformChunk lastPlatofrmChunk)
    {
        int platformsCount = lastPlatofrmChunk.GetPlatformsCount();
        int gapsCount = lastPlatofrmChunk.GetGapsCount();
        float chunkZEnd = (platformsCount + gapsCount) * Platform.LENGTH;
        chunkZEnd  = lastPlatofrmChunk.CalculateLength();
        Vector3 position = lastPlatofrmChunk.transform.position;
        position.z += chunkZEnd;
        return position;
    }

    private PlatformChunk GetLastPlatformChunk()
    {
        PlatformChunk lastPlatformChunk = null;
        PlatformChunk[] platformChunks = FindObjectsOfType<PlatformChunk>();
        lastPlatformChunk = platformChunks.Where(ch => Mathf.Approximately(ch.transform.position.x, _targetChunk.transform.position.x)).OrderBy(ch => ch.gameObject.transform.position.z).Last();
        return lastPlatformChunk;
    }

    private void PlaceChunkAt(Vector3 position)
    {
        _targetChunk.transform.position = position;
    }

    private void PlaceChunkAt(PlatformChunk platformChunk, Vector3 position)
    {
        platformChunk.transform.position = position;
    }
}

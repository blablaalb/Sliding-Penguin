using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using System.Linq;
using UnityEditor;

[CustomEditor(typeof(PlatformChildDetector))]
public class PlatformChildDetectorEditor : Editor
{
    private PlatformChildDetector _detector;
    private PlatformChild[] _childs;

    public override void OnInspectorGUI()
    {
        _detector = target as PlatformChildDetector;
        DrawDefaultInspector();

        if (GUILayout.Button("Check"))
        {
            PlatformChild[] occupyingChildren;
            if (_detector.PositionOccupied(out occupyingChildren))
            {
                _childs = occupyingChildren;
                foreach (var v in occupyingChildren)
                {
                    Debug.Log(v, v);
                }
            }
        }
    }


}
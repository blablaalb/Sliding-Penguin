using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class Bear : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)]
    private float _distanceTreshold = 1f;
    private Animator _bearAnimator;
    private bool _attacked;

    internal void Awake()
    {
        _bearAnimator = GetComponent<Animator>();
    }

    internal void Update()
    {
        if (!_attacked)
        {
            if (PenguinFSM.Instance.GetCurrentState() == PenguinStates.Sliding)
            {
                if (DistanceTowardPeguin() < _distanceTreshold)
                {
                    _attacked |= true;
                    _bearAnimator.CrossFade("Base Layer.Hit", 0.1f);
                }
            }
        }
    }

    public float DistanceTowardPeguin()
    {
        float distance = 0f;
        distance = Vector3.Distance(transform.position, Penguin.Instance.Position);
        distance = transform.position.z - Penguin.Instance.Position.z;
        return distance;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Bear))]
public class BearEditor : Editor
{
    private Bear _targetBear;
    private GUIStyle _style = new GUIStyle();

    public override void OnInspectorGUI()
    {
        _style.normal.textColor = Color.white;
        _targetBear = target as Bear;
        DrawDefaultInspector();

        if (EditorApplication.isPlaying || EditorApplication.isPaused)
        {
            EditorGUILayout.LabelField("Distance: ", _targetBear.DistanceTowardPeguin().ToString(), _style);
        }
    }
}
#endif
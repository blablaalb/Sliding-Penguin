using System.Collections.Generic;
using UnityEngine;
using System;

public class Gap : MonoBehaviour
{
    private Collider _collider;
    [SerializeField]
    private float _length;

    public float Length => _length;


    internal void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    public void DisableCollider()
    {
        _collider.enabled = false;
    }
}

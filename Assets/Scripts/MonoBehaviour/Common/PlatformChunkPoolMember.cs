using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using NaughtyAttributes;

public class PlatformChunkPoolMember : PoolMember<PlatformChunkManager>
{
    [SerializeField]
    private float _maxDistance = 10f;
    private PlatformChunk _platformChunks;
    private Platform _lastPlatform;

    override protected void Awake()
    {
        base.Awake();
        _platformChunks = GetComponent<PlatformChunk>();
        _lastPlatform = _platformChunks.GetLastPlatform();
    }

    internal void Update()
    {
        if (_lastPlatform.Position.z < Penguin.Instance.Position.z)
        {
            float distance = Vector3.Distance(Penguin.Instance.Position, _lastPlatform.Position);
            if (distance >= _maxDistance) ReturnToPool();
        }
    }
}
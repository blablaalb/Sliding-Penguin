using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using NaughtyAttributes;

public class PlatformChunkManager : GenericPool<PlatformChunk>
{
    [SerializeField]
    private PlatformChunk[] _platformChunks;
    private PlatformLengthObserver _platformLengthObserver;
    [Tooltip("Amount of chunks to spawn after player reached the treshold")]
    [SerializeField]
    private int _spawnAmount = 1;

    public PlatformChunk LastPlatformChunk { get; private set; }

    override protected void Awake()
    {
        base.Awake();
        _platformLengthObserver = GetComponent<PlatformLengthObserver>();
    }

    internal void Start()
    {
        _platformLengthObserver.ReachedThreshold += OnPenguinReachedPlatformEndTreshold;

        // IF we didn't preallocated chunks in the edtor spawn $_spawnAmount chunks.
        if (!FindLastPlatformChunk())
        {
            SpawnRandomPlatformChunks(_spawnAmount);
        }
    }

    internal void OnDestroy()
    {
        _platformLengthObserver.ReachedThreshold -= OnPenguinReachedPlatformEndTreshold;
    }

    [Button]
    private PlatformChunk SpawnRandomPlatformChunk()
    {
        var platformChunk = GetRandomPlatformChunk();
        platformChunk.SetParent(this.transform);
        Vector3 position = new Vector3(LastPlatformChunk.Position.x, LastPlatformChunk.Position.y, LastPlatformChunk.Position.z + LastPlatformChunk.CalculateLength());
        platformChunk.SetGlobalPosition(position);
        platformChunk.SetLocalRotation(Quaternion.identity);
        LastPlatformChunk = platformChunk;
        return platformChunk;
    }

    private PlatformChunk[] SpawnRandomPlatformChunks(int amount)
    {
        PlatformChunk[] chunks = new PlatformChunk[amount];
        for (int i = 0; i < amount; i++)
        {
            chunks[i] = SpawnRandomPlatformChunk();
        }
        return chunks;
    }

    private PlatformChunk GetRandomPlatformChunk()
    {
        int indx = Random.Range(0, _platformChunks.Length);
        PlatformChunk chunk = _platformChunks[indx];
        prefab = chunk.gameObject;
        chunk = Get();
        return chunk;
    }

    private bool FindLastPlatformChunk()
    {
        var platformChunks = FindObjectsOfType<PlatformChunk>();
        if (platformChunks == null || platformChunks.Length <= 0) return false;
        var last = platformChunks.OrderBy(x => x.Position.z).Last();
        LastPlatformChunk = last;
        return true;
    }

    private void OnPenguinReachedPlatformEndTreshold()
    {
        SpawnRandomPlatformChunks(_spawnAmount);
    }
}
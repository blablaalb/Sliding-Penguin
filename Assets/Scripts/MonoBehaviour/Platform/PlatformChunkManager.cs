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

        // IF we didn't preallocate chunks in the edtor spawn $_spawnAmount chunks.
        if (!FindLastPlatformChunk())
        {
            SpawnPlatformChunks(_spawnAmount, ChunkDifficulty.Zero);
        }
    }

    internal void OnDestroy()
    {
        _platformLengthObserver.ReachedThreshold -= OnPenguinReachedPlatformEndTreshold;
    }

#if UNITY_EDITOR
    [Button]
#endif
    private PlatformChunk SpawnPlatformChunk(ChunkDifficulty difficulty)
    {
        var platformChunk = GetPlatformChunk(difficulty);
        platformChunk.SetParent(this.transform);
        Vector3 position = new Vector3(LastPlatformChunk.Position.x, LastPlatformChunk.Position.y, LastPlatformChunk.Position.z + LastPlatformChunk.CalculateLength());
        platformChunk.SetGlobalPosition(position);
        platformChunk.SetLocalRotation(Quaternion.identity);
        LastPlatformChunk = platformChunk;
        return platformChunk;
    }

    private PlatformChunk[] SpawnPlatformChunks(int amount, ChunkDifficulty difficulty)
    {
        PlatformChunk[] chunks = new PlatformChunk[amount];
        for (int i = 0; i < amount; i++)
        {
            chunks[i] = SpawnPlatformChunk(difficulty);
        }
        return chunks;
    }

    private PlatformChunk GetPlatformChunk(ChunkDifficulty difficulty)
    {
        int indx = (int)difficulty;
        PlatformChunk chunk = null;
        switch (difficulty)
        {
            case ChunkDifficulty.Random:
                indx = Random.Range(0, _platformChunks.Length);
                chunk = _platformChunks[indx];
                break;
            case ChunkDifficulty.Zero:
                chunk = _platformChunks.First(x => x.Difficulty == 0);
                break;
            case ChunkDifficulty.One:
                chunk = _platformChunks.First(x => x.Difficulty == 1);
                break;
            case ChunkDifficulty.Two:
                chunk = _platformChunks.First(x => x.Difficulty == 2);
                break;
            case ChunkDifficulty.Three:
                chunk = _platformChunks.First(x => x.Difficulty == 3);
                break;
            default:
                Debug.LogException(new ArgumentException($"Unknown difficulty: {difficulty.ToString()}"));
                break;
        }
        prefab = chunk.gameObject;
        chunk = Get();
        if (!chunk.gameObject.activeInHierarchy) chunk.gameObject.SetActive(true);
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
        SpawnPlatformChunks(_spawnAmount, ChunkDifficulty.Zero);
    }

    private enum ChunkDifficulty
    {
        Random = -1,
        Zero,
        One,
        Two,
        Three
    }
}
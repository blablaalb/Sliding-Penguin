using System.Collections.Generic;
using UnityEngine;
using System;

public class PlatformChunk : MonoBehaviour
{
    [SerializeField]
    private int _difficulty;
    [SerializeField]
    private PlatformChunk[] _chunkVaritations;

    public int Difficulty => _difficulty;
    /// <summary>
    /// Amount of variations.
    /// </summary>
    public int PlatformVariationsCount => _chunkVaritations.Length;


    /// <summary>
    /// Returns number of platform instances that are in this chunk.
    /// </summary>
    /// <returns>Number of platform instances</returns>
    public int GetPlatformsCount()
    {
        Platform[] platforms = GetComponentsInChildren<Platform>();
        int count = platforms == null ? 0 : platforms.Length;
        return count;
    }

    /// <summary>
    /// Returns number of obstacles that are in this chunk.
    /// </summary>
    /// <returns>Number of obstacles</returns>
    public int GetObstaclesCount()
    {
        Obstacle[] obstacles = GetComponentsInChildren<Obstacle>();
        int count = obstacles == null ? 0 : obstacles.Length;
        return count;
    }

    /// <summary>
    /// Returns number of springboards that are in this chunk.
    /// </summary>
    /// <returns>Number of obstacles</returns>
    public int GetTrampolinesCount()
    {
        Springboard[] springboards = GetComponentsInChildren<Springboard>();
        int count = springboards == null ? 0 : springboards.Length;
        return count;
    }

    /// <summary>
    /// Returns number of gaps that are in this chunk.
    /// </summary>
    /// <returns>Number of gaps</returns>
    public int GetGapsCount()
    {
        Gap[] gaps = GetComponentsInChildren<Gap>();
        int count = gaps == null ? 0 : gaps.Length;
        return count;
    }

    public float CalculateLength()
    {
        int gaps = GetGapsCount();
        int platforms = GetPlatformsCount();
        float length = Platform.LENGTH * (gaps + platforms);
        return length;
    }

    /// <summary>
    /// Disables crrent chunk, instantiates a new one and return reference to it.
    /// </summary>
    /// <returns>Reference to the new instantiated platform chunk.</returns>
    public PlatformChunk SpawnRandomVariation()
    {
        if (PlatformVariationsCount <= 0)
        {
            return this.gameObject.GetComponent<PlatformChunk>();
        }
        int randomIndx = UnityEngine.Random.Range(0, _chunkVaritations.Length);
        PlatformChunk randomChunk = _chunkVaritations[randomIndx];
        randomChunk = Instantiate<PlatformChunk>(randomChunk);
        randomChunk.gameObject.transform.SetParent(transform.parent);
        gameObject.SetActive(false);
        return randomChunk;
    }

    // TODO: add score count.
}

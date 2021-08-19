using System.Collections.Generic;
using UnityEngine;
using System;

public class PlatformChunk : MonoBehaviour
{
    [SerializeField]
    private int _difficulty;

    public int Difficulty => _difficulty;

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

    // TODO: add score count.
}

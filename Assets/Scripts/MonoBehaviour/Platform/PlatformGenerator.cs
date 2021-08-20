using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class PlatformGenerator : Singleton<PlatformGenerator>
{
    [SerializeField]
    private Platform _platformPrefab;
    private PlatformLengthObserver _platformLengthObserver;

    public Platform LastPlatform { get; private set; }

    override protected void Awake()
    {
        base.Awake();
        _platformLengthObserver = GetComponent<PlatformLengthObserver>();
        FindLastPlatform();
    }

    public Platform[] AddPlatforms(int amount)
    {
        Platform[] platforms = new Platform[amount];
        for (int i = 0; i < amount; i++)
        {
            platforms[i] = AddPlatform();
        }
        return platforms;
    }

    public Platform AddPlatform()
    {
        Vector3 position = Vector3.zero;
        Quaternion rotation = Quaternion.identity;
        if (LastPlatform != null)
        {
            position = LastPlatform.CalculatePlatformEnd();
            rotation = LastPlatform.Rotation;
        }
        Platform platform = Instantiate<Platform>(_platformPrefab);
        platform.SetParent(this.gameObject);
        platform.SetPosition(position);
        platform.SetRotation(rotation);
        LastPlatform = platform;
        return platform;
    }

    private bool FindLastPlatform()
    {
        var platforms = FindObjectsOfType<Platform>();
        if (platforms == null || platforms.Length <= 0) return false;
        var last = platforms.OrderBy(x => x.Position.z).Last();
        LastPlatform = last;
        return true;
    }

}

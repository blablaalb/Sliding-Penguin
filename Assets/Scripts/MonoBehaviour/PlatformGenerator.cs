using System.Collections.Generic;
using UnityEngine;
using System;

public class PlatformGenerator : Singleton<PlatformGenerator>
{
    [SerializeField]
    private Platform _platformPrefab;

    public Platform LastPlatform { get; private set; }

    private Platform GeneratePlatform()
    {
        Platform platform = AddPlatform();
        return platform;
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
}

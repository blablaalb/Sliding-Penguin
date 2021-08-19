using System.Collections.Generic;
using UnityEngine;
using System;

public class PlatformGenerator : Singleton<PlatformGenerator>
{
    [SerializeField]
    private Platform _platformPrefab;
    private Penguin _penguin;

    public Platform LastPlatform { get; private set; }

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            // GeneratePlatform();
        }
    }

    // NOTE: This method is for debugging purpose only. Remove it afterwards.
    private Platform GeneratePlatform()
    {
        Platform platform = AddPlatform();
        // AddIcicleObstacleFull(platform);
        AddScoreObject(platform);
        // AddSpringboard(platform);
        return platform;
    }

    private void AddIcicleObstacleFull(Platform platform)
    {
        Obstacle obstacle = ObstacleManager.Instance.GetObstacle(ObstacleType.IcicleSignle);
        platform.PlaceChild(obstacle);
        obstacle.Spawn();
    }

    private void AddSpringboard(Platform platform)
    {
        Springboard accelerator = SpringboardManager.Instance.GetSpringboard();
        platform.PlaceChild(accelerator);
    }

    private void AddScoreObject(Platform platform)
    {
        Score score = ScoreManager.Instance.GetScore();
        platform.PlaceChild(score);
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
        // mark the new generated platform as the new one
        LastPlatform = platform;
        return platform;
    }
}

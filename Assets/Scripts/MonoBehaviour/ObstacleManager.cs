using System.Collections.Generic;
using UnityEngine;
using System;

public class ObstacleManager : Singleton<ObstacleManager>
{
    [SerializeField]
    private GameObject _icicleSinglePrefab;
    [SerializeField]
    private GameObject _icicleFullPrefab;

    public Obstacle GetObstacle(ObstacleType type)
    {
        Obstacle obstacle = null;
        GameObject obstacleGO = null;
        switch (type)
        {
            case ObstacleType.IcicleSignle:
                obstacleGO = Instantiate<GameObject>(_icicleSinglePrefab);
                break;
            case ObstacleType.IcicleFull:
                obstacleGO = Instantiate<GameObject>(_icicleFullPrefab);
                break;
        }
        obstacle = obstacleGO.GetComponentInChildren<Obstacle>();
        return obstacle;
    }
    
}

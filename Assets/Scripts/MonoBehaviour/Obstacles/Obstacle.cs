using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Obstacle : PlatformChild
{
    public static Action<Obstacle> ObstacleHit;

    virtual protected void OnDestroy()
    {
        ObstacleHit = null;
    }

    virtual protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Penguin>() is Penguin penguin)
        {
            ObstacleHit?.Invoke(this);
        }
    }

}
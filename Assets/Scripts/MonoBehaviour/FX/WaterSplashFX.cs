using System.Collections.Generic;
using UnityEngine;
using System;

public class WaterSplashFX : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    internal void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void Play()
    {
        _particleSystem.Play();
    }

    public void Stop()
    {
        _particleSystem.Stop();
    }
}

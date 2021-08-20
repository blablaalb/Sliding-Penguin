using System.Collections.Generic;
using UnityEngine;
using System;

public class SnowSurfingFX : MonoBehaviour
{
    private ParticleSystem[] _particleSystems;

    internal void Awake()
    {
        _particleSystems = GetComponentsInChildren<ParticleSystem>();
        foreach (var ps in _particleSystems)
        {
            ps.playOnAwake = false;
        }
    }

    public void Play()
    {
        transform.parent = Penguin.Instance.transform;
        transform.localPosition = Vector3.zero;
        foreach (ParticleSystem ps in _particleSystems)
        {
            ps.Play();
        }
    }

    public void Stop()
    {
        foreach (ParticleSystem ps in _particleSystems)
        {
            ps.Stop();
        }
        transform.parent = null;
    }
}

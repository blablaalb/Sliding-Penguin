using System;
using System.Collections.Generic;
using UnityEngine;

public class Icicle : Obstacle
{
    private IcicleParticleSystem _icicleParticleSys;
    private MeshRenderer[] _meshRenderers;

    override protected void Awake()
    {
        _meshRenderers = GetComponentsInChildren<MeshRenderer>();
        _icicleParticleSys = GetComponent<IcicleParticleSystem>();
        base.Awake();
    }

    override protected void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
        _icicleParticleSys.Play();
        Disable();
    }

    private void Disable()
    {
        foreach (var mr in _meshRenderers)
        {
            mr.enabled = false;
        }
    }
}

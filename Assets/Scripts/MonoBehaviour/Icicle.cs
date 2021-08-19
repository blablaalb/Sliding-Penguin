using System;
using System.Collections.Generic;
using UnityEngine;

public class Icicle : Obstacle
{
    [SerializeField]
    private float _raiseTime = 1f;
    [SerializeField]
    private IcicleParticleSystem _dustParticleSystem;

    private void Start()
    {
        // Raise();
    }

    public void Raise()
    {
        LeanTween.moveLocalY(this.gameObject, 0f, _raiseTime);
        _dustParticleSystem.Play();
    }

    public override void Spawn()
    {
        // Raise();
    }

}

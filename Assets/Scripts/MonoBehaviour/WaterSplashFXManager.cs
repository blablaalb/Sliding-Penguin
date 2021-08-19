using System.Collections.Generic;
using UnityEngine;
using System;

public class WaterSplashFXManager : MonoBehaviour
{
    private WaterSplashFXPool _waterSplashPool;

    internal void Awake()
    {
        _waterSplashPool = GetComponent<WaterSplashFXPool>();
    }

    internal void Start()
    {
        PenguinFSM.Instance.StateChanged += OnStateChanged;
    }

    internal void OnDestroy()
    {
        if (PenguinFSM.Instance != null)
        {
            PenguinFSM.Instance.StateChanged -= OnStateChanged;
        }
    }

    private void OnStateChanged(PenguinStates state)
    {
        if (state == PenguinStates.Land)
        {
            WaterSplashFX waterSplashParticleSystem = _waterSplashPool.Get();
            Vector3 landPosition = Penguin.Instance.Position;
            waterSplashParticleSystem.SetPosition(landPosition);
            waterSplashParticleSystem.Play();
        }
    }
}

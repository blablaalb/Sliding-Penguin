using System.Collections.Generic;
using UnityEngine;
using System;

public class SnowSurfingFXManager : Singleton<SnowSurfingFXManager>
{
    private SnowSurfingFXPool _snowSurfingFXPool;
    private SnowSurfingFX _lastPooledFX;

    override protected void Awake()
    {
        base.Awake();
        _snowSurfingFXPool = GetComponent<SnowSurfingFXPool>();
    }

    internal void Start()
    {
        if (PenguinFSM.Instance.GetCurrentState() == PenguinStates.Sliding)
        {
            Play();
        }
        PenguinFSM.Instance.StateChanged += OnPenguinStateChanged;
    }

    internal void OnDestory()
    {
        if (PenguinFSM.Instance != null)
        {
            PenguinFSM.Instance.StateChanged -= OnPenguinStateChanged;
        }
    }

    private void OnPenguinStateChanged(PenguinStates state)
    {
        switch (state)
        {
            case PenguinStates.Sliding:
                Play();
                break;
            case PenguinStates.Ascend:
                Stop();
                break;
            case PenguinStates.Fly:
                Stop();
                break;
            case PenguinStates.Descend:
                Stop();
                break;
            case PenguinStates.Land:
                Stop();
                break;
            case PenguinStates.OnGap:
                Stop();
                break;
            case PenguinStates.SlideOnBellyUp:
                Stop();
                break;
            case PenguinStates.SlideOnBellyHorizontal:
                Stop();
                break;
            case PenguinStates.StandUpFromBellySlide:
                Stop();
                break;
            case PenguinStates.None:
                Stop();
                break;
        }
    }

    private void Play()
    {
        _lastPooledFX = _snowSurfingFXPool.Get();
        _lastPooledFX.Play();
    }

    private void Stop()
    {
        _lastPooledFX?.Stop();
    }
}
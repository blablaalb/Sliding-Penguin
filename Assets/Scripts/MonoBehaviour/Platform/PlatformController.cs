﻿using System.Collections.Generic;
using UnityEngine;
using System;
using NaughtyAttributes;

public class PlatformController : MonoBehaviour
{
    [SerializeField]
    private float _maxRotationAngleClamped = 90f;
    [SerializeField]
    private float _maxRotationAngleUnclamped = 110f;
    [SerializeField]
    private bool _run;
    [SerializeField]
    private float _rotateTime = 0.5f;
    private float _targetZ;

    public float TargetRotation => _targetZ;

    /// <summary>
    /// Invoked when the player rotates the platform. Provides direction of the rotation. -1 for left, 1 for right and 0 for no rotation.
    /// </summary>
    public event Action<int> PlatformRotated;
    public event Action<int> Flipped;


    internal void Awake()
    {
        _run = true;
        Obstacle.ObstacleHit += OnObstacleHit;
    }

    internal void Start()
    {
        InputManager.Instance.Swiped += OnPlayerSwiped;
        PenguinFSM.Instance.StateChanged += OnPenguinStateChanged;
    }

    internal void OnDestroy()
    {
        Obstacle.ObstacleHit -= OnObstacleHit;
        if (PenguinFSM.Instance != null)
        {
            PenguinFSM.Instance.StateChanged -= OnPenguinStateChanged;
        }
        if (InputManager.Instance != null)
        {
            InputManager.Instance.Swiped -= OnPlayerSwiped;
        }
    }

    private void OnPlayerSwiped(SwipeDirection direction) { }

    private void OnPenguinStateChanged(PenguinStates state) { }

    private void OnObstacleHit(Obstacle obstacle)
    {
        _run = false;
    }

#if UNITY_EDITOR
    [Button]
#endif
    // rotates the platform RIGHT so the penguin appears on the left side of the screen
    public void RotateQuarterLeft()
    {
        float newTargetZ = _targetZ;
        if (newTargetZ >= -_maxRotationAngleClamped && newTargetZ <= _maxRotationAngleClamped)
        {
            if (newTargetZ >= -_maxRotationAngleUnclamped && newTargetZ < _maxRotationAngleClamped)
                newTargetZ += _maxRotationAngleClamped;
            else if (newTargetZ == _maxRotationAngleClamped)
                newTargetZ = _maxRotationAngleUnclamped;
        }
        else // if penguin is flying
        {
            if (newTargetZ == -_maxRotationAngleUnclamped) newTargetZ = -_maxRotationAngleClamped;
        }
        if (newTargetZ != _targetZ)
        {
            _targetZ = newTargetZ;
            LeanTween.cancel(this.gameObject);
            LeanTween.rotateZ(this.gameObject, _targetZ, _rotateTime).setOnComplete(() => SetRotation(_targetZ));
            if (newTargetZ == _maxRotationAngleUnclamped) Flipped?.Invoke(-1);
            else
                PlatformRotated?.Invoke(-1);
        }
    }

#if UNITY_EDITOR
    [Button]
# endif
    // rotates the platform LEFT so the penguin appears on the right side of the screen
    public void RotateQuarterRight()
    {
        float newTargetZ = _targetZ;
        if (newTargetZ >= -_maxRotationAngleClamped && newTargetZ <= _maxRotationAngleClamped)
        {
            if (newTargetZ > -_maxRotationAngleClamped && newTargetZ <= _maxRotationAngleUnclamped)
                newTargetZ -= _maxRotationAngleClamped;
            else if (newTargetZ == -_maxRotationAngleClamped)
                newTargetZ = -_maxRotationAngleUnclamped;
        }
        else // if penguin is flying
        {
            if (newTargetZ == _maxRotationAngleUnclamped) newTargetZ = _maxRotationAngleClamped;
        }
        if (newTargetZ != _targetZ)
        {
            _targetZ = newTargetZ;
            LeanTween.cancel(this.gameObject);
            LeanTween.rotateZ(this.gameObject, _targetZ, _rotateTime).setOnComplete(() => SetRotation(_targetZ));
            if (newTargetZ == -_maxRotationAngleUnclamped) Flipped?.Invoke(1);
            else
                PlatformRotated?.Invoke(1);
        }
    }

    private void SetRotation(float z)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, z));
    }
}

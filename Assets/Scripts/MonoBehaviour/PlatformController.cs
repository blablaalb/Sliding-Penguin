﻿using System.Collections.Generic;
using UnityEngine;
using System;

public class PlatformController : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed;
    [SerializeField]
    private float _maxRotationAngleClamped = 90f;
    [SerializeField]
    private float _maxRotationAngleUnclamped = 110f;
    private float _minRotation;
    private float _maxRotation;
    [SerializeField]
    private bool _run;
    private bool _canSwipe;
    private float _zCurrent = 0f;

    /// <summary>
    /// Invoked when the player rotates the platform. Provides direction of the rotation. -1 for left, 1 for right and 0 for no rotation.
    /// </summary>
    public event Action<int> PlatformMoved;

    internal void Awake()
    {
        _run = true;
        _canSwipe = true;
        _minRotation = 0 - _maxRotationAngleClamped;
        _maxRotation = _maxRotationAngleClamped;
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

    internal void FixedUpdate()
    {
        if (!_run)
        {
            return;
        }

        HandleInput();
    }

    private void HandleInput()
    {
        float x = InputManager.Instance.GetXAxis();
        float zAxisRotation = x * Time.deltaTime * _rotateSpeed;
        _zCurrent -= zAxisRotation;

        _zCurrent = Mathf.Clamp(_zCurrent, _minRotation, _maxRotation);
        transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, _zCurrent));


        int direction = 0;
        if (x < 0.000f)
        {
            direction = -1;
        }
        else if (x > 0.000f)
        {
            direction = 1;
        }
        PlatformMoved?.Invoke(direction);
    }

    private void OnPlayerSwiped(SwipeDirection direction)
    {
        if (PenguinFSM.Instance.GetCurrentState() == PenguinStates.Sliding)
        {
            if (_canSwipe)
            {
                _canSwipe = false;
                _run = false;
                Rotate(direction);
            }
        }
    }

    private void OnPenguinStateChanged(PenguinStates state)
    {
        switch (state)
        {
            case PenguinStates.Descend:
                ResetMinMaxRotations();
                break;
            case PenguinStates.Land:
                _canSwipe = true;
                // ResetMinMaxRotations();
                break;
        }
    }

    private void ResetMinMaxRotations()
    {
        float targetMinRotation = 0 - _maxRotationAngleClamped;
        float targetMaxRotation = _maxRotationAngleClamped;
        LeanTween.value(this.gameObject, _minRotation, targetMinRotation, 0.2f).setOnUpdate(x => _minRotation = x);
        LeanTween.value(this.gameObject, _maxRotation, targetMaxRotation, 0.2f).setOnUpdate(x => _maxRotation = x);
    }

    private void Rotate(SwipeDirection direction)
    {
        _minRotation = 0 - _maxRotationAngleUnclamped;
        _maxRotation = _maxRotationAngleUnclamped;
        float targetZRot = direction == SwipeDirection.Left ? _maxRotationAngleUnclamped : 0 - _maxRotationAngleUnclamped;

        transform.LeanRotateZ(targetZRot, 0.2f).setOnComplete(() => { _run = true; _zCurrent = targetZRot; });

        Debug.Log("rotating");
    }

    private void OnObstacleHit(Obstacle obstacle)
    {
        _run = false;
    }
}

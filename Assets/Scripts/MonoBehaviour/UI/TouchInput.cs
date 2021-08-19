using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchInput : Singleton<TouchInput>
{
    private Vector2 _startPosition;
    private Vector2 _currentPosition;
    // Max X is the half of the child image
    private float _maxX;
    // Min x is the negative of the max x
    private float _minX;
    private float _startTime;
    [SerializeField, Range(0f, 5f)]
    private float _swipeTimeTreshold = 1f;
    private bool _canSwipe;
    private float _previousX;
    private float _xVelocity;
    [SerializeField]
    private float _smooth = 0.5f;
    private Vector2 _previousPosition;


    /// <summary>
    /// Called when player touches the screen. Provides position of the screen.
    /// </summary>
    public event Action<Vector2> Touched;
    /// <summary>
    /// Called when player swiped on the screen (touch + swipe + pull finger up). Provides swipe speed.
    /// </summary>
    public event Action<SwipeDirection> Swiped;

    protected override void Awake()
    {
        base.Awake();
        _maxX = Screen.width / 3;
        _minX = 0 - _maxX;
        _canSwipe = true;
    }

    public float GetXAxis()
    {
        float x = Mathf.Clamp(DeltaPosition().x, -1, 1);
        float rawx = x;
        if (x != 0f)
            x = Mathf.SmoothDamp(_previousX, x, ref _xVelocity, _smooth);
        _previousX = x;
        return x;
    }

    internal void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch firstTouch = Input.GetTouch(0);
            _previousPosition = _currentPosition;
            switch (firstTouch.phase)
            {
                case TouchPhase.Began:
                    OnTouchBegan(firstTouch.position);
                    break;
                case TouchPhase.Moved:
                    _currentPosition = firstTouch.position;
                    OnTouchMoved();
                    break;
                case TouchPhase.Stationary:
                    _currentPosition = firstTouch.position;
                    OnTouchStationary();
                    break;
                case TouchPhase.Ended:
                    OnTouchEnded();
                    break;
                case TouchPhase.Canceled:
                    OnTouchEnded();
                    break;
            }
        }
    }

    private void OnTouchBegan(Vector2 position)
    {
        _startTime = Time.time;
        _startPosition = position;
        Touched?.Invoke(_startPosition);
    }

    private void ResetValues()
    {
        _previousPosition = Vector2.zero;
        _startPosition = Vector2.zero;
        _currentPosition = Vector2.zero;
        _canSwipe = true;
        _startTime = -1;
    }

    private void OnTouchStationary()
    {
        _startPosition = _currentPosition;
        _startTime = Time.time;
        _canSwipe = true;
    }

    private void OnTouchMoved()
    {
    }

    private void OnTouchEnded()
    {
        if (_canSwipe && _startTime > 0)
        {
            Vector2 deltaPosition = _currentPosition - _startPosition;
            float swipeTime = Time.time - _startTime;
            if (Mathf.Abs(deltaPosition.x) >= _maxX)
            {
                if (swipeTime <= _swipeTimeTreshold)
                {
                    SwipeDirection direction = deltaPosition.x > 0.00f ? SwipeDirection.Right : SwipeDirection.Left;
                    Swiped?.Invoke(direction);
                    _startTime = Time.time;
                    _canSwipe = false;
                }
            }
        }
        ResetValues();
    }

    private Vector2 DeltaPosition() => _currentPosition - _previousPosition;
}

public enum SwipeDirection
{
    Left,
    Right
}

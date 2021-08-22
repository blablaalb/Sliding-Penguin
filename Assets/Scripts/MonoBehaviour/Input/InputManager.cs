using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : Singleton<InputManager>
{
    [Range(0f, 5f)]
    [SerializeField]
    private float _sensitivity;
    private PlatformController _platformController;
    private IInput _input;

    public event Action<float> SensitivityChanged;
    public event Action<SwipeDirection> Swiped;


    override protected void Awake()
    {
#if (UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE)
        _input = KeyboardInput.Instance;
#elif (UNITY_ANDROID || UNITY_IPHONE)
        _input = TouchInput.Instance;
#endif

        base.Awake();
        _platformController = FindObjectOfType<PlatformController>();
    }

    internal void Start()
    {
        _input.Swiped += OnSwiped;
        SetSensitivity(1.5f);
    }

    override protected void OnDestroy()
    {
        if (_input != null)
            _input.Swiped -= OnSwiped;
    }

    internal void Update()
    {
        float x = GetXAxis();
        if (x < 0) _platformController.RotateQuarterLeft();
        else 
        if (x > 0) _platformController.RotateQuarterRight();
    }

    public float GetXAxis()
    {
        float x = 0f;
        x = _input.GetXAxis();
        x *= _sensitivity;
        return x;
    }

    public void SetSensitivity(float sensitivity)
    {
        _sensitivity = sensitivity;
        SensitivityChanged?.Invoke(_sensitivity);
    }

    private void OnSwiped(SwipeDirection direction)
    {
        Swiped?.Invoke(direction);
    }
}

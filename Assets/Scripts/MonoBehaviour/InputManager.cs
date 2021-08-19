using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : Singleton<InputManager>
{
    [Range(0f, 5f)]
    [SerializeField]
    private float _sensitivity;

    public event Action<float> SensitivityChanged;
    public event Action<SwipeDirection> Swiped;

    internal void Start()
    {
        TouchInput.Instance.Swiped += OnSwiped;
        SetSensitivity(1f);
    }

    override protected void OnDestroy()
    {
        if (TouchInput.Instance != null)
            TouchInput.Instance.Swiped -= OnSwiped;
    }

    public float GetXAxis()
    {
        float x = 0f;
#if (UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE)
        x = Input.GetAxis("Horizontal");
#elif (UNITY_ANDROID || UNITY_IPHONE)
        x = TouchInput.Instance.GetXAxis();
#endif
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

#if UNITY_EDITOR
    internal void Update()
    {
        // Reset scene with ctrl + shift + r
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
                }
            }
        }

        // Simulate swipe on keyboard.
        if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnSwiped(SwipeDirection.Right);
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnSwiped(SwipeDirection.Left);
            }
        }
    }
#endif
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SensitivitySlider : MonoBehaviour
{
    private Slider _slider;

    internal void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    internal void Start()
    {
        InputManager.Instance.SensitivityChanged += OnSensitivityChanged;
    }

    internal void OnDestroy()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.SensitivityChanged -= OnSensitivityChanged;
        }
    }

    private void OnSensitivityChanged(float sensitivity)
    {
        _slider.value = sensitivity;
    }
}

using System.Collections.Generic;
using UnityEngine;
using System;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private float _defaultTimeScale = 1f;
    private float _beforePauseTimeScale;
    private bool _paused;

    private void Start()
    {
        if (!_paused)
        {
            _beforePauseTimeScale = _defaultTimeScale;
        }
    }

    public void Pause()
    {
        _paused = true;
        _beforePauseTimeScale = Time.timeScale;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        _paused = false;
        Time.timeScale = _beforePauseTimeScale;
    }

}

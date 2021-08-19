using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : PersistentSingleton<GameManager>
{
    private PauseManager _pauseManager;

    public Action Paused;
    public Action Resumed;

    override protected void Awake()
    {
        base.Awake();

        _pauseManager = GetComponent<PauseManager>();
    }

    public void Pause()
    {
        _pauseManager.Pause();
        Paused?.Invoke();
    }

    public void Resume()
    {
        _pauseManager.Resume();
        Resumed?.Invoke();
    }
}

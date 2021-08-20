using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using MEC;

public class LevelManager : Singleton<LevelManager>
{
    public Action LevelLoaded;

    internal void Start()
    {
        LevelLoaded?.Invoke();
    }

    public void RestartLevel(float delay = 0f)
    {
        Timing.RunCoroutine(RestartLevelCoroutine(delay));
    }

    private IEnumerator<float> RestartLevelCoroutine(float delay)
    {
        yield return Timing.WaitForSeconds(delay);
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name, LoadSceneMode.Single);
    }
}

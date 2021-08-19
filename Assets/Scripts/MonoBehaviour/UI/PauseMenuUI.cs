using System.Collections.Generic;
using UnityEngine;
using System;

public class PauseMenuUI : Singleton<PauseMenuUI>, IUIScreen
{
    internal void Start()
    {
        Hide();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        GameManager.Instance.Resume();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        GameManager.Instance.Pause();
    }

    public void Switch()
    {
        if (gameObject.activeSelf)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }
}

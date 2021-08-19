using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GameEndScreenUI : Singleton<GameEndScreenUI>, IUIScreen
{
    [SerializeField]
    private TextMeshProUGUI _tmPro;

    override protected void Awake()
    {
        base.Awake();
        Obstacle.ObstacleHit += OnObstacleHit;
        ScoreCounter.Instance.ScoreCollected += OnScoreCollected;
    }

    internal void Start()
    {
        Hide();
    }

    override protected void OnDestroy()
    {
        if (ScoreCounter.Instance != null)
        {
            ScoreCounter.Instance.ScoreCollected -= OnScoreCollected;
        }
        Obstacle.ObstacleHit -= OnObstacleHit;
        base.OnDestroy();
    }

    private void OnObstacleHit(Obstacle obstacle)
    {
        Show();
    }

    private void OnScoreCollected(Score scoreObj)
    {
        UpdateScoreText();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void UpdateScoreText()
    {
        float score = ScoreCounter.Instance.Collected;
        _tmPro.SetText(score.ToString());
    }
}

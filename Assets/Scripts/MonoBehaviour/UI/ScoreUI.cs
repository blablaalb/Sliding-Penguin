using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    private TextMeshProUGUI _tmPro;

    internal void Awake()
    {
        _tmPro = GetComponentInChildren<TextMeshProUGUI>();
        ScoreCounter.Instance.ScoreCollected += OnScoreCollected;
    }

    internal void OnDestroy()
    {
        if (ScoreCounter.Instance != null)
        {
            ScoreCounter.Instance.ScoreCollected -= OnScoreCollected;
        }
    }

    private void OnScoreCollected(Score score)
    {
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        float score = ScoreCounter.Instance.Collected;
        _tmPro.SetText(score.ToString());
    }
}

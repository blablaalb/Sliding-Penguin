using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ScoreCollectFXManager : MonoBehaviour
{
    private CollectScoreOnScreenFXPool _scoreFXPool;
    [SerializeField]
    private Image _scoreUIImage;

    internal void Awake()
    {
        _scoreFXPool = GetComponent<CollectScoreOnScreenFXPool>();
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
        SlideScore(score);
    }

    private void SlideScore(Score score)
    {
        Vector3 scoreWorldPosition = score.Position;
        scoreWorldPosition = Penguin.Instance.Position;
        Vector2 scoreScreenPosition = Camera.main.WorldToScreenPoint(scoreWorldPosition);
        Vector2 scoreTargetPosition = _scoreUIImage.rectTransform.position;
        CollectScoreOnScreenFX scoreFX = _scoreFXPool.Get();
        scoreFX.Slide(scoreScreenPosition, scoreTargetPosition);
    }
}

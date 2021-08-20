using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreCounter : Singleton<ScoreCounter>
{
    public float Collected { get; private set; }

    public event Action<Score> ScoreCollected;

    public void CollectScore(Score score)
    {
        Collected += score.Amount;
        ScoreCollected?.Invoke(score);
    }
}

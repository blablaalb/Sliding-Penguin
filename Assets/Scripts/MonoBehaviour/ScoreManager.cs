using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField]
    private GameObject _scorePrefab;

    public Score GetScore()
    {
        GameObject scoreGO = Instantiate<GameObject>(_scorePrefab);
        Score score = scoreGO.GetComponentInChildren<Score>();
        return score;
    }
}

using System.Collections.Generic;
using UnityEngine;
using System;

public class Score : PlatformChild, ICollectable
{
    [SerializeField]
    private float _amount = 10f;
    [SerializeField]
    private AudioClip _collectAudioClip;

    public float Amount => _amount;
    public Vector3 Position => transform.position;

    public void CollectSelf()
    {
        ScoreCounter.Instance.CollectScore(this);
        AudioClipPlayer.Instance.PlayAudioClip(_collectAudioClip);
        gameObject.SetActive(false);
    }

}

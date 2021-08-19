using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using MEC;

public class CollectScoreOnScreenFX : MonoBehaviour
{
    private RectTransform _rectTransform;
    private float _currentTime;
    private float _targetTime;
    private float _speed;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _currentTime = 0f;
        _targetTime = 5f;
        _speed = 0.05f;
    }

    public void Slide(Vector2 from, Vector2 to)
    {
        Timing.RunCoroutine(SlideCoroutie(from, to).CancelWith(gameObject));
    }


    private IEnumerator<float> SlideCoroutie(Vector2 from, Vector2 to)
    {
        _currentTime = 0f;
        float t = 0;
        _rectTransform.position = from;
        while (_currentTime < _targetTime)
        {
            t += Time.deltaTime * _speed;
            _currentTime = Mathf.Lerp(_currentTime, _targetTime, t);
            _rectTransform.position = Vector2.Lerp(_rectTransform.position, to, t);
            yield return Timing.WaitForOneFrame;
        }
    }
}

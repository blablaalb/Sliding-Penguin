using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform _leftPosition;
    [SerializeField]
    private Transform _rightPosition;
    [SerializeField]
    private Transform _middlePosition;
    [SerializeField]
    private float _transitionTime = 1.5f;
    private PlatformController _platformController;

    internal void Awake()
    {
        _platformController = FindObjectOfType<PlatformController>();
    }

    internal void Start()
    {
        _platformController.Flipped += OnPlatformFlipped;
        _platformController.PlatformRotated += OnPlatformRotated;
    }

    internal void OnDestroy()
    {
        if (_platformController != null)
        {
            _platformController.Flipped -= OnPlatformFlipped;
            _platformController.PlatformRotated -= OnPlatformRotated;
        }
    }

    private void OnPlatformFlipped(int direction)
    {
        if (direction == -1) MoveLeft();
        else
        if (direction == 1) MoveRight();
    }

    private void OnPlatformRotated(int direction)
    {
        MoveMiddle();
    }

    private void MoveLeft()
    {
        LeanTween.moveLocal(this.gameObject, _leftPosition.localPosition, _transitionTime);
        LeanTween.rotateLocal(this.gameObject, _leftPosition.localRotation.eulerAngles, _transitionTime);
    }

    private void MoveRight()
    {
        LeanTween.moveLocal(this.gameObject, _rightPosition.localPosition, _transitionTime);
        LeanTween.rotateLocal(this.gameObject, _rightPosition.localRotation.eulerAngles, _transitionTime);
    }

    private void MoveMiddle()
    {
        LeanTween.moveLocal(this.gameObject, _middlePosition.localPosition, _transitionTime);
        LeanTween.rotateLocal(this.gameObject, _middlePosition.localRotation.eulerAngles, _transitionTime);
    }
}
